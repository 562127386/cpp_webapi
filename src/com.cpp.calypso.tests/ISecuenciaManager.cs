using System.Threading.Tasks;

namespace com.cpp.calypso.tests
{
    public interface ISecuenciaManager
    {
        Task<long> GetNextSequenceAsync(string codeSequence);

        long GetNextSequence(string codeSequence);

        Task<long> GetNextSequenceSQL(string codeSequence);

        Task<long> GetNextSequenceSQLWithLock(string codeSequence);
    }
}