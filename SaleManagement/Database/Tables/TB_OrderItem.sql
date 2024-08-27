create table TB_OrderItem(
	OrderId varchar(32),
    ProductId varchar(32),
    Count integer,
    UnitValue decimal(14,2)not null,
    Total decimal(14,2)not null,
    ProductName varchar(100),
    primary key(OrderId, ProductId),
    constraint FK_TB_OrderItem_TB_Order foreign key (OrderId) references TB_Order(Id)
    
);