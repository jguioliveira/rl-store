db.createUser(
    {
        user: "adminuser",
        pwd: "qwerty11",
        roles: [
            {
                role: "readWrite",
                db: "UserstoreDb"
            }
        ]
    }
);

db.User.insertOne({ email: "jose@gmail.com", firstName: "Jose", lastName: "Oliveira", password: "123456" } );