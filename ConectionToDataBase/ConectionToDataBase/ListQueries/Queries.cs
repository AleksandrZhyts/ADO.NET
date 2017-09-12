
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
            "WHERE visitdoctor = doctorID AND visitPatient = patientID " +
            "GROUP BY doctorName " +
            "UNION " +
            "SELECT d.doctorName , NULL " +
            "FROM Doctors d " +
            "WHERE NOT EXISTS (SELECT visitDoctor FROM Visits WHERE visitdoctor = d.doctorID) ";

        public static string query8 = 
            "SELECT doctorName AS [Доктор], SUM(visitCostValue) AS [Сумма] " +
            "FROM VisitCosts, Doctors, Visits " +
            "WHERE visitdoctor = doctorID AND visitID = visitCostID  " +
            "GROUP BY doctorName " +
            "UNION " +
            "SELECT doctorName AS [Доктор], NULL " +
            "FROM Doctors d " +
            "WHERE NOT EXISTS (SELECT visitDoctor FROM VisitCosts, Visits WHERE visitCostId = visitID AND visitdoctor = d.doctorID) ";
       
/*запрос о заказах клиентов - данные о клиенте (имя компании, контактное имя, адрес, город, страна, телефон),
данные о заказе (дата заказа, дата поставки, стоимость перевозки, адрес поставки (город, страна, регион)), 
дополнительные данные о заказе (цена, количество, стоимость);*/
        public static string query9 =
            "SELECT CompanyName, ContactName, Address, City, Region, Phone,  " +
                    "OrderDate, ShippedDate, Freight, ShipCity, ShipCountry, ShipRegion, " +
                    "UnitPrice, Quantity, UnitPrice*Quantity AS Total " +
            "FROM Customers INNER JOIN (Orders INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID) " +
            "ON Customers.CustomerID = Orders.CustomerID ";


        /*запрос о продуктах, цены которых в интервале от 10 до 60 и которые в настоящий момент не сняты с производства. 
        В запросе должна быть информация (Название продукта, цена, снято/не снято,  количество в упаковке, имя категории)*/
        public static string query10 =
            "SELECT ProductName, UnitPrice, Discontinued AS [Снято/Не снято], QuantityPerUnit, CategoryName " +
            "FROM Products INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID " +
            "WHERE UnitPrice BETWEEN 10 AND 60;";

/*запрос, формирующий информацию о количестве клиентов в городе 
Пример: город: Минск, количество клиентов: 50
        город: Москва, количество клиентов: 126 */
        public static string query11 =
            "SELECT City AS [Город], COUNT(*) AS [Количество клиентов] " +
            "FROM Customers INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
            "GROUP BY City ";

        /*запрос, выводящий информацию о клиентах  с указанием общей суммы заказов за заданный период:
            Необходимо вывести: имя компании,  город, страна,  дата заказа, сумма.*/
        public static string query12 =
            "SELECT CompanyName, City, Country, OrderDate, SUM(UnitPrice * Quantity) AS [Сумма] " +
            "FROM Customers LEFT JOIN (Orders INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID) " +
            "ON Customers.CustomerID = Orders.CustomerID " +
            //"WHERE OrderDate BETWEEN '09.15.1996' AND '10.01.1996' " +
            "Group BY CompanyName, City, Country, OrderDate ";
 
    }
}
