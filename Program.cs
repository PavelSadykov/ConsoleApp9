
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Data.SQLite;

namespace ConsoleApp9
{


    class Program
    {
        // Определяем делегат для записи в базу данных
        delegate void WriteToDatabaseDelegate(string name, int age);

        static void Main(string[] args)
        {
            // Создаем соединение с базой данных
            SQLiteConnection connectionString = new SQLiteConnection("Data Source=C:/Users/TH/Test.db;Version=3;");
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                // Открытие соединения с базой данных
                connection.Open();
                Console.WriteLine("Соединеник с базой данных установлено");


                // Создаем таблицу для хранения данных
                var command = new SQLiteCommand("CREATE TABLE Persone ( Name TEXT,   Age  INTEGER  );", connection);
                command.ExecuteNonQuery();

                // Определяем метод для записи в базу данных
                WriteToDatabaseDelegate writeToDatabase = (string _name, int _age) =>
                {
                    // Создаем команду для вставки данных
                    command = new SQLiteCommand($"INSERT INTO Persone (Name, Age) VALUES ('{_name}', {_age})", connection);
                    command.ExecuteNonQuery();
                };

                // Считываем данные с консоли
                Console.Write("Введите имя: ");
                var name = Console.ReadLine();

                Console.Write("Введите возраст: ");
                var age = int.Parse(Console.ReadLine());

                // Выводим данные на экран
                Console.WriteLine($"Имя: {name}, Возраст: {age}");

                // Записываем данные в базу данных
                writeToDatabase(name, age);

                // Закрываем соединение с базой данных
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка во время выполнения : " + ex.Message);
            }




            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();

        }

    }

}


