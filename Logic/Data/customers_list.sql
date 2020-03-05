SELECT Customers.Id, FirstName + ' ' + LastName as Name, Age, Street, City, ZipCode FROM 
Customers INNER JOIN Addresses
ON Addresses.Id = (
        SELECT  TOP 1 Id
        FROM    Addresses
		WHERE CustomerId = Customers.Id
        ORDER BY  IsPrimary DESC
         );