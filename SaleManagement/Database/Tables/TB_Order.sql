create table TB_Order(
	Id varchar(32)not null,
    CustmomerId varchar(32),
    Status tinyint,
    Total decimal(14,2)not null,
    Created datetime not null,
    Updated datetime not null,
    PaymentForm tinyint,
    primary key(Id)
);

