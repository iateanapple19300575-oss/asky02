' Program.vb
Imports System
Imports System.Data

Module Program

    Sub Main()

        ' ★ 接続文字列を環境に合わせて変更してください
        Dim cs As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"

        Dim userRepo As New UserRepository(cs)
        Dim orderRepo As New OrderRepository(cs)

        Console.WriteLine("=== INSERT USER ===")
        userRepo.InsertUser("Taro", "taro@example.com")
        Console.WriteLine("User inserted.")

        Console.WriteLine()
        Console.WriteLine("=== SELECT USER ===")
        Dim userDt As DataTable = userRepo.GetUserById(1)

        For Each row As DataRow In userDt.Rows
            Console.WriteLine("ID: " & row(Tables.Users.UserId))
            Console.WriteLine("Name: " & row(Tables.Users.UserName))
            Console.WriteLine("Email: " & row(Tables.Users.Email))
        Next

        Console.WriteLine()
        Console.WriteLine("=== SELECT ORDERS (UserId = 1) ===")
        Dim orderDt As DataTable = orderRepo.GetOrdersByUserId(1)

        For Each row As DataRow In orderDt.Rows
            Console.WriteLine("OrderId: " & row(Tables.Orders.OrderId))
            Console.WriteLine("OrderDate: " & row(Tables.Orders.OrderDate))
            Console.WriteLine("Amount: " & row(Tables.Orders.Amount))
            Console.WriteLine("-----------------------------")
        Next

        Console.WriteLine()
        Console.WriteLine("=== SELECT USER ===")
        Dim userDt2 As DataTable = userRepo.GetUserOrdersByUserId(1)

        For Each row As DataRow In userDt2.Rows
            Console.WriteLine("UserName: " & row(Tables.Users.UserName))
            Console.WriteLine("OrderDate: " & row(Tables.Orders.OrderDate))
            Console.WriteLine("Amount: " & row(Tables.Orders.Amount))
        Next



        Console.WriteLine("Done.")
        Console.ReadLine()

    End Sub

End Module