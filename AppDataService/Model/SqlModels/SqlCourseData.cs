using AccountService.Model.Base;

namespace AccountService.Model.SqlModels
{
    public class SqlCourseData
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public int TotalPeriodosCurso { get; set; }
    }
}
