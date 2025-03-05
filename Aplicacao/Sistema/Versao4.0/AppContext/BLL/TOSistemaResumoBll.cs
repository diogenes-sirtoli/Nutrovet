namespace BLL
{
    public class TOSistemaDWHBll
    {
        public int? TotalAlimentos { get; set; }
        public int? TotalCardapios { get; set; }
        public int? TotalTutores { get; set; }
        public int? TotalPacientes { get; set; }

        public TOSistemaDWHBll()
        {
            TotalAlimentos = 0;
            TotalCardapios = 0;
            TotalTutores = 0;
            TotalPacientes = 0;
        }
    }
}
