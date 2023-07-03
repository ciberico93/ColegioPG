namespace AppColegioPG.Models
{
    public class Alumnos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
        public string Correo { get; set; }

        public int Id_Cursos { get; set; }

    }
}
