using Foundatio.Storage;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Campusoft.Nucleo.Presentacion
{
    /// <summary>
    /// Sobreescribir, para lanzar excepcion al momento de intentar escribir archivos.
    /// </summary>
    public class FolderFileStorageOverwrite : IFileStorage
    {
        private readonly object _lockObject = new object();

        public FolderFileStorageOverwrite(string folder)
        {
            folder = PathHelperOverwrite.ExpandPath(folder);

            if (!Path.IsPathRooted(folder))
                folder = Path.GetFullPath(folder);
            if (!folder.EndsWith("\\"))
                folder += "\\";

            Folder = folder;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public string Folder { get; set; }

        public async Task<Stream> GetFileStreamAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            try
            {
                if (!await ExistsAsync(path).AnyContext())
                    return null;

                return File.OpenRead(Path.Combine(Folder, path));
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public Task<FileSpec> GetFileInfoAsync(string path)
        {
            var info = new FileInfo(path);
            if (!info.Exists)
                return Task.FromResult<FileSpec>(null);

            return Task.FromResult(new FileSpec
            {
                Path = path.Replace(Folder, String.Empty),
                Created = info.CreationTime,
                Modified = info.LastWriteTime,
                Size = info.Length
            });
        }

        public Task<bool> ExistsAsync(string path)
        {
            return Task.FromResult(File.Exists(Path.Combine(Folder, path)));
        }

        public Task<bool> SaveFileAsync(string path, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            string directory = Path.GetDirectoryName(Path.Combine(Folder, path));
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            try
            {
                using (var fileStream = File.Create(Path.Combine(Folder, path)))
                {
                    if (stream.CanSeek)
                        stream.Seek(0, SeekOrigin.Begin);

                    stream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                //Change: Lanzar excepcion
                //return Task.FromResult(false);
                throw ex;
            }

            return Task.FromResult(true);
        }

        public Task<bool> RenameFileAsync(string path, string newpath, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));
            if (String.IsNullOrWhiteSpace(newpath))
                throw new ArgumentNullException(nameof(newpath));

            try
            {
                lock (_lockObject)
                {
                    string directory = Path.GetDirectoryName(newpath);
                    if (directory != null && !Directory.Exists(Path.Combine(Folder, directory)))
                        Directory.CreateDirectory(Path.Combine(Folder, directory));

                    File.Move(Path.Combine(Folder, path), Path.Combine(Folder, newpath));
                }
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> CopyFileAsync(string path, string targetpath, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));
            if (String.IsNullOrWhiteSpace(targetpath))
                throw new ArgumentNullException(nameof(targetpath));

            try
            {
                lock (_lockObject)
                {
                    string directory = Path.GetDirectoryName(targetpath);
                    if (directory != null && !Directory.Exists(Path.Combine(Folder, directory)))
                        Directory.CreateDirectory(Path.Combine(Folder, directory));

                    File.Copy(Path.Combine(Folder, path), Path.Combine(Folder, targetpath));
                }
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteFileAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            try
            {
                File.Delete(Path.Combine(Folder, path));
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<IEnumerable<FileSpec>> GetFileListAsync(string searchPattern = null, int? limit = null, int? skip = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit.HasValue && limit.Value <= 0)
                return Task.FromResult<IEnumerable<FileSpec>>(new List<FileSpec>());

            if (String.IsNullOrEmpty(searchPattern))
                searchPattern = "*";

            var list = new List<FileSpec>();
            if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(Folder, searchPattern))))
                return Task.FromResult<IEnumerable<FileSpec>>(list);

            foreach (var path in Directory.GetFiles(Folder, searchPattern, SearchOption.AllDirectories).Skip(skip ?? 0).Take(limit ?? Int32.MaxValue))
            {
                var info = new FileInfo(path);
                if (!info.Exists)
                    continue;

                list.Add(new FileSpec
                {
                    Path = path.Replace(Folder, String.Empty),
                    Created = info.CreationTime,
                    Modified = info.LastWriteTime,
                    Size = info.Length
                });
            }

            return Task.FromResult<IEnumerable<FileSpec>>(list);
        }

        public void Dispose() { }
    }


    internal static class TaskExtensionsOverwrite
    {
        public static Task WaitAsync(this AsyncCountdownEvent countdownEvent, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.WhenAny(countdownEvent.WaitAsync(), cancellationToken.AsTask());
        }

        //public static Task WaitAsync(this AsyncCountdownEvent countdownEvent, TimeSpan timeout)
        //{
        //    return countdownEvent.WaitAsync(timeout.ToCancellationToken());
        //}

        //public static Task WaitAsync(this AsyncManualResetEvent resetEvent, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return Task.WhenAny(resetEvent.WaitAsync(), cancellationToken.AsTask());
        //}

        //public static Task WaitAsync(this AsyncManualResetEvent resetEvent, TimeSpan timeout)
        //{
        //    return resetEvent.WaitAsync(timeout.ToCancellationToken());
        //}

        //public static Task WaitAsync(this AsyncAutoResetEvent resetEvent, TimeSpan timeout)
        //{
        //    return resetEvent.WaitAsync(timeout.ToCancellationToken());
        //}

        public static Task IgnoreExceptions(this Task task)
        {
            task.ContinueWith(c => { var ignored = c.Exception; },
                TaskContinuationOptions.OnlyOnFaulted |
                TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }

        public static Task<T> IgnoreExceptions<T>(this Task<T> task)
        {
            task.ContinueWith(c => { var ignored = c.Exception; },
                TaskContinuationOptions.OnlyOnFaulted |
                TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }

        public static void SetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task task)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion: resultSetter.SetResult(task is Task<TResult> ? ((Task<TResult>)task).Result : default(TResult)); break;
                case TaskStatus.Faulted: resultSetter.SetException(task.Exception.InnerExceptions); break;
                case TaskStatus.Canceled: resultSetter.SetCanceled(); break;
                default: throw new InvalidOperationException("The task was not completed.");
            }
        }

        public static void SetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task<TResult> task)
        {
            SetFromTask(resultSetter, (Task)task);
        }

        public static bool TrySetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task task)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion: return resultSetter.TrySetResult(task is Task<TResult> ? ((Task<TResult>)task).Result : default(TResult));
                case TaskStatus.Faulted: return resultSetter.TrySetException(task.Exception.InnerExceptions);
                case TaskStatus.Canceled: return resultSetter.TrySetCanceled();
                default: throw new InvalidOperationException("The task was not completed.");
            }
        }

        public static bool TrySetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task<TResult> task)
        {
            return TrySetFromTask(resultSetter, (Task)task);
        }

        [DebuggerStepThrough]
        public static ConfiguredTaskAwaitable<TResult> AnyContext<TResult>(this Task<TResult> task)
        {
            return task.ConfigureAwait(continueOnCapturedContext: false);
        }

        [DebuggerStepThrough]
        public static ConfiguredTaskAwaitable AnyContext(this Task task)
        {
            return task.ConfigureAwait(continueOnCapturedContext: false);
        }
    }

    internal static class PathHelperOverwrite
    {
        private const string DATA_DIRECTORY = "|DataDirectory|";

        public static string ExpandPath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return path;

            if (!path.StartsWith(DATA_DIRECTORY, StringComparison.OrdinalIgnoreCase))
                return Path.GetFullPath(path);

            string dataDirectory = GetDataDirectory();
            int length = DATA_DIRECTORY.Length;

            if (path.Length <= length)
                return dataDirectory;

            string relativePath = path.Substring(length);
            char c = relativePath[0];

            if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
                relativePath = relativePath.Substring(1);

            string fullPath = Path.Combine(dataDirectory, relativePath);
            fullPath = Path.GetFullPath(fullPath);

            return fullPath;
        }

        public static string GetDataDirectory()
        {
            string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (String.IsNullOrEmpty(dataDirectory))
                dataDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return Path.GetFullPath(dataDirectory);
        }
    }
}
