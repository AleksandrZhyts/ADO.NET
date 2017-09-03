
namespace ConectionToDataBase.ListQueries
{
    public static class Queries
    {
        public static string query1 = "SELECT * FROM Patients";
        public static string query2 = "SELECT * FROM Doctors";
        //public string static query = "SELECT * FROM Visits";
        //public string static query0 = "SELECT * FROM VisitCosts";
        public static string query3 = 
            "SELECT patientFirstName&' '&patientName AS [Пациент], doctorFirstName&' '&doctorName AS [Доктор] " +
            "FROM Patients p, Doctors d " +
            "WHERE patientID = (SELECT visitPatient FROM Doctors, Visits " +
                                "WHERE visitDoctor = doctorID AND doctorID = d.doctorID AND visitPatient = p.patientID )";

        public static string query4 = 
            "SELECT patientName AS [Пациент], doctorName AS [Доктор], visitCostValue AS [Цена] " +
            "FROM Patients p, Doctors d, VisitCosts v " +
            "WHERE patientID = (SELECT visitPatient FROM Doctors, Visits, VisitCosts " +
                                "WHERE visitDoctor = doctorID AND doctorID = d.doctorID " +
                                    "AND visitPatient = p.patientID  " +
                                    "AND visitCostID = visitID AND visitID = v.visitCostID)";

        public static string query5 = 
            "SELECT doctorName AS [Доктор], visitComment AS [Диагноз], visitCostFrom&'-'&visitCostTill AS [Дата] " +
            "FROM Doctors d, Visits v, VisitCosts vc WHERE doctorID = visitDoctor AND visitID = visitCostID";

        public static string query6 =
            "SELECT patientName AS [Пациент], doctorName AS [Доктор], visitCostValue AS [Цена] " +
            "FROM Patients, Doctors, Visits, VisitCosts " +
            "WHERE visitdoctor = doctorID AND visitPatient = patientID AND visitCostID = visitID  " +
                "AND visitCostValue = (SELECT MAX(visitCostValue) " +
                                    "FROM Patients, Doctors, Visits, VisitCosts " +
                                    "WHERE visitCostID = visitID AND visitPatient = patientID AND visitdoctor = doctorID) ";

        public static string query7 = 
            "SELECT doctorName AS [Доктор], COUNT(patientID) AS [Количество] " +
            "FROM Patients, Doctors, Visits " +
            "WHERE visitdoctor = doctorID AND visitPatient = patientID  " +
            "GROUP BY doctorName";

        public static string query8 = 
            "SELECT doctorName AS [Доктор], SUM(visitCostValue) AS [Количество] " +
            "FROM VisitCosts, Doctors, Visits " +
            "WHERE visitdoctor = doctorID AND visitID = visitCostID  " +
            "GROUP BY doctorName";
    }            
}
