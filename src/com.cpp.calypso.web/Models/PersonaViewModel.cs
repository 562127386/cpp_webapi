namespace com.cpp.calypso.web
{
    public class PersonaViewModel
    {
        public string Id { get; set; }
        public string Cedula { get; set; }
        public string Pasaporte { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public string GetFullName(bool includeId = false)
        {
            return includeId ? ModelHelper.GetFullName(Nombres, Apellidos, Id) : ModelHelper.GetFullName(Nombres, Apellidos);
        }
    }
}
