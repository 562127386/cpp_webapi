namespace com.cpp.calypso.web
{
    ///// <summary>
    ///// Clase para gestion de identidades de usuarios
    ///// </summary>
    //public class GenericIdentityUser : IIdentityUser
    //{

    //    IApplication _application;

    //     public GenericIdentityUser(IApplication application)
    //    {
    //        _application = application;
    //    }


    //    /// <summary>
    //    /// Obtener el identificador de la identidad del usuario
    //    /// </summary>
    //    /// <returns></returns>
    //    public int GetCurrentIdentity()
    //    {

    //        //TODO: JSA, definir si se guarda el identificador del usuario o su nombre
    //        //Opcion 1. Guardar el identificador en el nombre del usuario
    //        //Opcion 2. Recuperar el usuario actual desde la aplicacion

    //        //string identityName = Thread.CurrentPrincipal.Identity.Name;
    //        int identityUsuario = -1; //TODO: JSA, guardar en configruacion
    //        //int.TryParse(identityName, out identityUsuario);

    //        var user = _application.GetCurrentUser();
    //        if (user != null)
    //            identityUsuario = user.Id;

    //        return identityUsuario;
    //    }
    //}
}
