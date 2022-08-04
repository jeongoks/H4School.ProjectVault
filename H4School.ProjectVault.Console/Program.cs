﻿using H4School.ProjectVault.Service.DTO;
using H4School.ProjectVault.Service.Services;
using System.Text;

class Program
{
    static string menuChoice;
    static PasswordDTO password = new();
    static void Main()
    {
        while (menuChoice != "3")
        {
            Console.Clear();
            Console.WriteLine("1. Add a password.");
            Console.WriteLine("2. Show your now hashed password.");
            Console.WriteLine("3. Exit application.");
            Console.WriteLine("Please select any of the above to start an action:");
            menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    Console.Clear();
                    UserInputPassword();
                    break;
                case "2":
                    Console.Clear();
                    ShowHashedPassword();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
            }
        };
    }

    static void UserInputPassword()
    {
        Console.WriteLine("Write your wanted password: ");
        string userPassword = Console.ReadLine();
        Console.WriteLine("Write the file name you want to save the password in: ");
        password.FileName = Console.ReadLine();

        password.Salt = HashingPassword.GenerateSalt();
        password.HashedPassword = HashingPassword.HashingPasswordWithSalt(Encoding.UTF8.GetBytes(userPassword), password.Salt);

        FileHandler.SavePasswordInFile(password);

        Console.WriteLine($"Your password has now been hashed and saved in a file.\n File name: {password.FileName}");
        Console.WriteLine("Press Enter to return to the menu!");
        Console.ReadLine();
    }

    static void ShowHashedPassword()
    {
        Console.WriteLine($"This is the hashed version of your password: {Convert.ToBase64String(password.HashedPassword)}");
        Console.WriteLine("Press enter to return to menu!");
        Console.ReadLine();
    }
}