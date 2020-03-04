SELECT * FROM 
Customers INNER JOIN Addresses
ON Addresses.Id = (
        SELECT  TOP 1 Id
        FROM    Addresses
		WHERE CustomerId = CustomerId
        ORDER BY  IsPrimary DESC
         );