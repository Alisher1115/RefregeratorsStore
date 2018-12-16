using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace RefregeratorStoreHw
{
    class Program
    {
        static int OrderNumberKey;

        static Program()
        {
            OrderNumberKey = 0;
        }

        public static void Main(string[] args)
        {
            DataSet dataSet = new DataSet("RefregeratorStore");
            DataSetMethod(dataSet);
            int customersId = CustomersList(dataSet);
            int key = 0;
            string keyAsString = "";
            while (key != 4)
            {
                while (!int.TryParse(keyAsString, out key))
                {
                    Console.WriteLine("\n\t--- Главное меню ---");
                    Console.WriteLine("\tПерейти к выбору холодильников - 1");
                    Console.WriteLine("\tПерейти к списку выбранных холодильников - 2");
                    Console.WriteLine("\tОформить заказ - 3");
                    Console.WriteLine("\tВыйти из приложения - 4");
                    Console.Write("\nВведите команду: ");
                    keyAsString = Console.ReadLine();
                    if ((!int.TryParse(keyAsString, out key)) || (key < 1) || (key > 4))
                    {
                        Console.WriteLine("\n\t --- Пожалуйста введите одну из нижеперечисленных команд ---");
                    }
                    else
                    {
                        switch (key)
                        {
                            case 1:
                                GoodsList(dataSet, customersId);
                                break;
                            case 2:
                                CartList(dataSet, customersId);
                                break;
                            case 3:
                                Buy(dataSet, customersId);
                                break;
                            case 4:
                                return;
                            default: break;
                        }
                    }
                    keyAsString = "";
                }
            }
            Console.ReadKey(true);
        }

        //
        static void Buy()
        {

        }

        //Метод устанавливающий тестовые значения
        static DataSet DataSetMethod(DataSet dataSet)
        {
            var goods = new[] {
                new { Id = 1, Name = "Duis", ManufacturerId = 19, CategoryId = 20, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 32 },
                new { Id = 2, Name = "vitae", ManufacturerId = 41, CategoryId = 28, Description = "Lorem ipsum dolor sit amet,", Price = 80 },
                new { Id = 3, Name = "at", ManufacturerId = 74, CategoryId = 55, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 92 },
                new { Id = 4, Name = "eu", ManufacturerId = 69, CategoryId = 97, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 96 },
                new { Id = 5, Name = "vitae", ManufacturerId = 54, CategoryId = 2, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 23 },
                new { Id = 6, Name = "ante", ManufacturerId = 21, CategoryId = 96, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 43 },
                new { Id = 7, Name = "viverra.", ManufacturerId = 30, CategoryId = 35, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 75 },
                new { Id = 8, Name = "vulputate", ManufacturerId = 78, CategoryId = 16, Description = "Lorem ipsum dolor sit amet,", Price = 63 },
                new { Id = 9, Name = "felis", ManufacturerId = 73, CategoryId = 1, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 61 },
                new { Id = 10, Name = "Morbi", ManufacturerId = 77, CategoryId = 71, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 88 },
                new { Id = 11, Name = "enim", ManufacturerId = 90, CategoryId = 73, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 22 },
                new { Id = 12, Name = "Maecenas", ManufacturerId = 71, CategoryId = 2, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 41 },
                new { Id = 13, Name = "a", ManufacturerId = 98, CategoryId = 16, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 46 },
                new { Id = 14, Name = "Sed", ManufacturerId = 92, CategoryId = 68, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 21 },
                new { Id = 15, Name = "eu", ManufacturerId = 28, CategoryId = 41, Description = "Lorem ipsum dolor sit amet,", Price = 60 },
                new { Id = 16, Name = "sodales", ManufacturerId = 37, CategoryId = 54, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 91 },
                new { Id = 17, Name = "nunc", ManufacturerId = 30, CategoryId = 72, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 98 },
                new { Id = 18, Name = "augue", ManufacturerId = 30, CategoryId = 60, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 76 },
                new { Id = 19, Name = "nunc", ManufacturerId = 85, CategoryId = 60, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 57 },
                new { Id = 20, Name = "tellus", ManufacturerId = 84, CategoryId = 99, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 98 },
                new { Id = 21, Name = "diam", ManufacturerId = 31, CategoryId = 92, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 47 },
                new { Id = 22, Name = "amet", ManufacturerId = 17, CategoryId = 16, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 46 },
                new { Id = 23, Name = "faucibus", ManufacturerId = 88, CategoryId = 22, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 13 },
                new { Id = 24, Name = "et", ManufacturerId = 79, CategoryId = 67, Description = "Lorem ipsum dolor sit amet,", Price = 96 },
                new { Id = 25, Name = "a,", ManufacturerId = 11, CategoryId = 99, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 85 },
                new { Id = 26, Name = "nisl", ManufacturerId = 64, CategoryId = 15, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 58 },
                new { Id = 27, Name = "luctus", ManufacturerId = 68, CategoryId = 34, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 23 },
                new { Id = 28, Name = "Pellentesque", ManufacturerId = 59, CategoryId = 40, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 27 },
                new { Id = 29, Name = "pede.", ManufacturerId = 41, CategoryId = 72, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 10 },
                new { Id = 30, Name = "Suspendisse", ManufacturerId = 80, CategoryId = 54, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 80 },
                new { Id = 31, Name = "non,", ManufacturerId = 11, CategoryId = 21, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 13 },
                new { Id = 32, Name = "non", ManufacturerId = 96, CategoryId = 8, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 23 },
                new { Id = 33, Name = "risus.", ManufacturerId = 54, CategoryId = 89, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 36 },
                new { Id = 34, Name = "Integer", ManufacturerId = 43, CategoryId = 24, Description = "Lorem ipsum dolor sit amet,", Price = 92 },
                new { Id = 35, Name = "nec", ManufacturerId = 83, CategoryId = 17, Description = "Lorem ipsum dolor sit amet,", Price = 71 },
                new { Id = 36, Name = "Ut", ManufacturerId = 75, CategoryId = 97, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 20 },
                new { Id = 37, Name = "sapien,", ManufacturerId = 66, CategoryId = 71, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 75 },
                new { Id = 38, Name = "dolor", ManufacturerId = 70, CategoryId = 91, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 55 },
                new { Id = 39, Name = "sodales", ManufacturerId = 30, CategoryId = 38, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 83 },
                new { Id = 40, Name = "ullamcorper,", ManufacturerId = 98, CategoryId = 97, Description = "Lorem ipsum dolor sit amet,", Price = 65 },
                new { Id = 41, Name = "mauris", ManufacturerId = 66, CategoryId = 52, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 44 },
                new { Id = 42, Name = "sociis", ManufacturerId = 26, CategoryId = 24, Description = "Lorem ipsum dolor sit amet,", Price = 58 },
                new { Id = 43, Name = "per", ManufacturerId = 55, CategoryId = 100, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 31 },
                new { Id = 44, Name = "Quisque", ManufacturerId = 98, CategoryId = 60, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 86 },
                new { Id = 45, Name = "leo.", ManufacturerId = 38, CategoryId = 1, Description = "Lorem ipsum dolor sit amet,", Price = 54 },
                new { Id = 46, Name = "et", ManufacturerId = 37, CategoryId = 85, Description = "Lorem ipsum dolor sit amet,", Price = 19 },
                new { Id = 47, Name = "justo", ManufacturerId = 22, CategoryId = 68, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 77 },
                new { Id = 48, Name = "Nunc", ManufacturerId = 70, CategoryId = 88, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 73 },
                new { Id = 49, Name = "mattis", ManufacturerId = 41, CategoryId = 92, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 31 },
                new { Id = 50, Name = "risus,", ManufacturerId = 11, CategoryId = 30, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 44 },
                new { Id = 51, Name = "nec", ManufacturerId = 45, CategoryId = 2, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 84 },
                new { Id = 52, Name = "ut", ManufacturerId = 53, CategoryId = 61, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 18 },
                new { Id = 53, Name = "luctus", ManufacturerId = 86, CategoryId = 12, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 69 },
                new { Id = 54, Name = "amet", ManufacturerId = 71, CategoryId = 77, Description = "Lorem ipsum dolor sit amet,", Price = 48 },
                new { Id = 55, Name = "Aenean", ManufacturerId = 50, CategoryId = 32, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 75 },
                new { Id = 56, Name = "vitae", ManufacturerId = 83, CategoryId = 48, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 12 },
                new { Id = 57, Name = "sapien.", ManufacturerId = 17, CategoryId = 50, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 89 },
                new { Id = 58, Name = "non,", ManufacturerId = 9, CategoryId = 67, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 24 },
                new { Id = 59, Name = "Nullam", ManufacturerId = 64, CategoryId = 21, Description = "Lorem ipsum dolor sit amet,", Price = 61 },
                new { Id = 60, Name = "fermentum", ManufacturerId = 67, CategoryId = 43, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 32 },
                new { Id = 61, Name = "sem", ManufacturerId = 95, CategoryId = 26, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 23 },
                new { Id = 62, Name = "Integer", ManufacturerId = 37, CategoryId = 48, Description = "Lorem ipsum dolor sit amet,", Price = 10 },
                new { Id = 63, Name = "quis,", ManufacturerId = 93, CategoryId = 37, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 42 },
                new { Id = 64, Name = "Pellentesque", ManufacturerId = 83, CategoryId = 57, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 99 },
                new { Id = 65, Name = "diam.", ManufacturerId = 49, CategoryId = 26, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 22 },
                new { Id = 66, Name = "Vivamus", ManufacturerId = 11, CategoryId = 51, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 59 },
                new { Id = 67, Name = "enim.", ManufacturerId = 78, CategoryId = 8, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 10 },
                new { Id = 68, Name = "sem", ManufacturerId = 99, CategoryId = 31, Description = "Lorem ipsum dolor sit amet,", Price = 20 },
                new { Id = 69, Name = "Donec", ManufacturerId = 55, CategoryId = 15, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 41 },
                new { Id = 70, Name = "magna.", ManufacturerId = 38, CategoryId = 58, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 77 },
                new { Id = 71, Name = "diam.", ManufacturerId = 34, CategoryId = 14, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 72 },
                new { Id = 72, Name = "tincidunt", ManufacturerId = 50, CategoryId = 50, Description = "Lorem ipsum dolor sit amet,", Price = 85 },
                new { Id = 73, Name = "magna", ManufacturerId = 54, CategoryId = 10, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 39 },
                new { Id = 74, Name = "urna.", ManufacturerId = 99, CategoryId = 8, Description = "Lorem ipsum dolor sit amet,", Price = 63 },
                new { Id = 75, Name = "mauris", ManufacturerId = 38, CategoryId = 8, Description = "Lorem ipsum dolor sit amet,", Price = 85 },
                new { Id = 76, Name = "ac", ManufacturerId = 87, CategoryId = 41, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 26 },
                new { Id = 77, Name = "in,", ManufacturerId = 40, CategoryId = 4, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 45 },
                new { Id = 78, Name = "parturient", ManufacturerId = 15, CategoryId = 57, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 73 },
                new { Id = 79, Name = "sagittis", ManufacturerId = 8, CategoryId = 6, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 14 },
                new { Id = 80, Name = "Etiam", ManufacturerId = 63, CategoryId = 33, Description = "Lorem ipsum dolor sit amet,", Price = 36 },
                new { Id = 81, Name = "et", ManufacturerId = 85, CategoryId = 79, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 77 },
                new { Id = 82, Name = "tellus", ManufacturerId = 12, CategoryId = 42, Description = "Lorem ipsum dolor sit amet,", Price = 38 },
                new { Id = 83, Name = "lectus", ManufacturerId = 25, CategoryId = 54, Description = "Lorem ipsum dolor sit amet,", Price = 62 },
                new { Id = 84, Name = "Aliquam", ManufacturerId = 26, CategoryId = 88, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 61 },
                new { Id = 85, Name = "erat", ManufacturerId = 13, CategoryId = 66, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 18 },
                new { Id = 86, Name = "Vivamus", ManufacturerId = 17, CategoryId = 9, Description = "Lorem ipsum dolor sit amet, consectetuer", Price = 20 },
                new { Id = 87, Name = "leo.", ManufacturerId = 81, CategoryId = 98, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 92 },
                new { Id = 88, Name = "ipsum", ManufacturerId = 48, CategoryId = 92, Description = "Lorem ipsum dolor sit amet,", Price = 78 },
                new { Id = 89, Name = "nunc", ManufacturerId = 100, CategoryId = 57, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 93 },
                new { Id = 90, Name = "neque.", ManufacturerId = 25, CategoryId = 75, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 36 },
                new { Id = 91, Name = "molestie", ManufacturerId = 99, CategoryId = 37, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 50 },
                new { Id = 92, Name = "libero.", ManufacturerId = 69, CategoryId = 49, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 83 },
                new { Id = 93, Name = "risus.", ManufacturerId = 35, CategoryId = 41, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", Price = 84 },
                new { Id = 94, Name = "ut", ManufacturerId = 57, CategoryId = 53, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 22 },
                new { Id = 95, Name = "Aenean", ManufacturerId = 40, CategoryId = 41, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 42 },
                new { Id = 96, Name = "tempus,", ManufacturerId = 95, CategoryId = 49, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed", Price = 66 },
                new { Id = 97, Name = "Proin", ManufacturerId = 82, CategoryId = 21, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur", Price = 72 },
                new { Id = 98, Name = "Donec", ManufacturerId = 62, CategoryId = 40, Description = "Lorem ipsum dolor sit amet,", Price = 11 },
                new { Id = 99, Name = "neque", ManufacturerId = 79, CategoryId = 45, Description = "Lorem ipsum dolor sit amet,", Price = 33 },
                new { Id = 100, Name = "Quisque", ManufacturerId = 5, CategoryId = 36, Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing", Price = 35 }
            };

            var manufacturers = new[] {
                new { Id = 1, Name = "ultricies" },
                new { Id = 2, Name = "ligula." },
                new { Id = 3, Name = "sem" },
                new { Id = 4, Name = "velit" },
                new { Id = 5, Name = "urna" },
                new { Id = 6, Name = "at," },
                new { Id = 7, Name = "aliquet" },
                new { Id = 8, Name = "vel," },
                new { Id = 9, Name = "faucibus." },
                new { Id = 10, Name = "magna" },
                new { Id = 11, Name = "nunc" },
                new { Id = 12, Name = "felis" },
                new { Id = 13, Name = "est." },
                new { Id = 14, Name = "dolor." },
                new { Id = 15, Name = "Nunc" },
                new { Id = 16, Name = "ipsum" },
                new { Id = 17, Name = "pharetra." },
                new { Id = 18, Name = "a," },
                new { Id = 19, Name = "vel" },
                new { Id = 20, Name = "Integer" },
                new { Id = 21, Name = "nec" },
                new { Id = 22, Name = "nec" },
                new { Id = 23, Name = "consectetuer" },
                new { Id = 24, Name = "Morbi" },
                new { Id = 25, Name = "pede." },
                new { Id = 26, Name = "arcu." },
                new { Id = 27, Name = "lacinia" },
                new { Id = 28, Name = "sem" },
                new { Id = 29, Name = "Cras" },
                new { Id = 30, Name = "turpis." },
                new { Id = 31, Name = "adipiscing" },
                new { Id = 32, Name = "erat" },
                new { Id = 33, Name = "viverra." },
                new { Id = 34, Name = "nec" },
                new { Id = 35, Name = "adipiscing" },
                new { Id = 36, Name = "ipsum" },
                new { Id = 37, Name = "et" },
                new { Id = 38, Name = "Maecenas" },
                new { Id = 39, Name = "ac" },
                new { Id = 40, Name = "metus." },
                new { Id = 41, Name = "lorem" },
                new { Id = 42, Name = "quis," },
                new { Id = 43, Name = "Suspendisse" },
                new { Id = 44, Name = "feugiat" },
                new { Id = 45, Name = "nibh" },
                new { Id = 46, Name = "pretium" },
                new { Id = 47, Name = "Mauris" },
                new { Id = 48, Name = "blandit" },
                new { Id = 49, Name = "tempus," },
                new { Id = 50, Name = "Aenean" },
                new { Id = 51, Name = "arcu." },
                new { Id = 52, Name = "habitant" },
                new { Id = 53, Name = "vel" },
                new { Id = 54, Name = "eget" },
                new { Id = 55, Name = "ornare" },
                new { Id = 56, Name = "amet," },
                new { Id = 57, Name = "vitae," },
                new { Id = 58, Name = "faucibus" },
                new { Id = 59, Name = "quis" },
                new { Id = 60, Name = "vehicula" },
                new { Id = 61, Name = "habitant" },
                new { Id = 62, Name = "tristique" },
                new { Id = 63, Name = "bibendum" },
                new { Id = 64, Name = "semper" },
                new { Id = 65, Name = "posuere" },
                new { Id = 66, Name = "Integer" },
                new { Id = 67, Name = "urna." },
                new { Id = 68, Name = "eu" },
                new { Id = 69, Name = "non," },
                new { Id = 70, Name = "dictum" },
                new { Id = 71, Name = "Nam" },
                new { Id = 72, Name = "Integer" },
                new { Id = 73, Name = "a" },
                new { Id = 74, Name = "mi" },
                new { Id = 75, Name = "Suspendisse" },
                new { Id = 76, Name = "convallis" },
                new { Id = 77, Name = "congue," },
                new { Id = 78, Name = "Nam" },
                new { Id = 79, Name = "magna" },
                new { Id = 80, Name = "malesuada" },
                new { Id = 81, Name = "Donec" },
                new { Id = 82, Name = "egestas" },
                new { Id = 83, Name = "cursus" },
                new { Id = 84, Name = "ipsum." },
                new { Id = 85, Name = "ultrices." },
                new { Id = 86, Name = "sagittis." },
                new { Id = 87, Name = "parturient" },
                new { Id = 88, Name = "sit" },
                new { Id = 89, Name = "Nunc" },
                new { Id = 90, Name = "amet" },
                new { Id = 91, Name = "ac" },
                new { Id = 92, Name = "elit" },
                new { Id = 93, Name = "et" },
                new { Id = 94, Name = "quam" },
                new { Id = 95, Name = "sed" },
                new { Id = 96, Name = "lacinia" },
                new { Id = 97, Name = "libero." },
                new { Id = 98, Name = "Cras" },
                new { Id = 99, Name = "orci." },
                new { Id = 100, Name = "sit" }
            };

            var categories = new[] {
                new { Id = 1, Name = "urna" },
                new { Id = 2, Name = "elit," },
                new { Id = 3, Name = "scelerisque," },
                new { Id = 4, Name = "semper," },
                new { Id = 5, Name = "pede," },
                new { Id = 6, Name = "scelerisque" },
                new { Id = 7, Name = "sit" },
                new { Id = 8, Name = "quam" },
                new { Id = 9, Name = "id" },
                new { Id = 10, Name = "dolor" },
                new { Id = 11, Name = "adipiscing" },
                new { Id = 12, Name = "aliquet," },
                new { Id = 13, Name = "egestas." },
                new { Id = 14, Name = "quam" },
                new { Id = 15, Name = "a" },
                new { Id = 16, Name = "feugiat" },
                new { Id = 17, Name = "id" },
                new { Id = 18, Name = "dolor," },
                new { Id = 19, Name = "lacus." },
                new { Id = 20, Name = "mauris," },
                new { Id = 21, Name = "felis" },
                new { Id = 22, Name = "egestas." },
                new { Id = 23, Name = "dolor" },
                new { Id = 24, Name = "quis," },
                new { Id = 25, Name = "diam" },
                new { Id = 26, Name = "sem" },
                new { Id = 27, Name = "ullamcorper," },
                new { Id = 28, Name = "vehicula." },
                new { Id = 29, Name = "nunc," },
                new { Id = 30, Name = "Vivamus" },
                new { Id = 31, Name = "parturient" },
                new { Id = 32, Name = "sed," },
                new { Id = 33, Name = "luctus" },
                new { Id = 34, Name = "Aliquam" },
                new { Id = 35, Name = "mauris" },
                new { Id = 36, Name = "Vestibulum" },
                new { Id = 37, Name = "nisl." },
                new { Id = 38, Name = "faucibus" },
                new { Id = 39, Name = "Duis" },
                new { Id = 40, Name = "tempor" },
                new { Id = 41, Name = "suscipit" },
                new { Id = 42, Name = "feugiat" },
                new { Id = 43, Name = "Proin" },
                new { Id = 44, Name = "Phasellus" },
                new { Id = 45, Name = "vitae" },
                new { Id = 46, Name = "imperdiet," },
                new { Id = 47, Name = "dapibus" },
                new { Id = 48, Name = "ipsum." },
                new { Id = 49, Name = "fermentum" },
                new { Id = 50, Name = "auctor." },
                new { Id = 51, Name = "dignissim" },
                new { Id = 52, Name = "pharetra" },
                new { Id = 53, Name = "Aliquam" },
                new { Id = 54, Name = "magna." },
                new { Id = 55, Name = "mi" },
                new { Id = 56, Name = "Morbi" },
                new { Id = 57, Name = "lacinia" },
                new { Id = 58, Name = "fames" },
                new { Id = 59, Name = "amet," },
                new { Id = 60, Name = "Sed" },
                new { Id = 61, Name = "scelerisque" },
                new { Id = 62, Name = "dis" },
                new { Id = 63, Name = "aliquet" },
                new { Id = 64, Name = "non," },
                new { Id = 65, Name = "bibendum" },
                new { Id = 66, Name = "nec," },
                new { Id = 67, Name = "diam." },
                new { Id = 68, Name = "arcu" },
                new { Id = 69, Name = "ac" },
                new { Id = 70, Name = "Sed" },
                new { Id = 71, Name = "tellus" },
                new { Id = 72, Name = "dui" },
                new { Id = 73, Name = "mauris" },
                new { Id = 74, Name = "non," },
                new { Id = 75, Name = "Proin" },
                new { Id = 76, Name = "enim." },
                new { Id = 77, Name = "nonummy." },
                new { Id = 78, Name = "felis." },
                new { Id = 79, Name = "tincidunt" },
                new { Id = 80, Name = "vulputate" },
                new { Id = 81, Name = "placerat" },
                new { Id = 82, Name = "elit," },
                new { Id = 83, Name = "consequat" },
                new { Id = 84, Name = "ante" },
                new { Id = 85, Name = "Suspendisse" },
                new { Id = 86, Name = "non," },
                new { Id = 87, Name = "ac" },
                new { Id = 88, Name = "eu" },
                new { Id = 89, Name = "leo." },
                new { Id = 90, Name = "risus" },
                new { Id = 91, Name = "Sed" },
                new { Id = 92, Name = "lorem," },
                new { Id = 93, Name = "turpis." },
                new { Id = 94, Name = "id" },
                new { Id = 95, Name = "vitae" },
                new { Id = 96, Name = "amet" },
                new { Id = 97, Name = "massa" },
                new { Id = 98, Name = "lectus," },
                new { Id = 99, Name = "fermentum" },
                new { Id = 100, Name = "interdum" }
            };

            var employees = new[] {
                new { Id = 1, FullName = "Zephania H. Brooks", Age = 29, Address = "Ap #963-6775 Lorem St.", Phone = "(02983) 8620492" },
                new { Id = 2, FullName = "Rebekah Cameron", Age = 27, Address = "585-4104 Ut St.", Phone = "(022) 44204433" },
                new { Id = 3, FullName = "Danielle Fry", Age = 18, Address = "P.O. Box 611, 8684 Enim Ave", Phone = "(0890) 04205537" },
                new { Id = 4, FullName = "Colby Allison", Age = 24, Address = "P.O. Box 167, 4978 Turpis. Avenue", Phone = "(06555) 5597798" },
                new { Id = 5, FullName = "Clementine Sparks", Age = 24, Address = "189-4607 Hymenaeos. Street", Phone = "(00044) 0472447" },
                new { Id = 6, FullName = "Devin Marquez", Age = 26, Address = "7679 Nam Avenue", Phone = "(036927) 695494" },
                new { Id = 7, FullName = "Keegan V. Thomas", Age = 20, Address = "Ap #646-6942 Fringilla St.", Phone = "(04240) 8901120" },
                new { Id = 8, FullName = "Quinn Guerra", Age = 30, Address = "4898 Mollis. Avenue", Phone = "(032) 47977954" },
                new { Id = 9, FullName = "Buffy B. Sutton", Age = 20, Address = "Ap #385-3845 Sem Rd.", Phone = "(02949) 1858292" },
                new { Id = 10, FullName = "Sigourney V. Kirk", Age = 19, Address = "639-3943 Blandit. Av.", Phone = "(01126) 1880045" },
                new { Id = 11, FullName = "Adrian Z. Salas", Age = 20, Address = "Ap #128-2831 Nam Rd.", Phone = "(0300) 88903566" },
                new { Id = 12, FullName = "Carson Tyson", Age = 21, Address = "405-8580 Donec Avenue", Phone = "(05474) 0257504" },
                new { Id = 13, FullName = "Leilani Haley", Age = 19, Address = "Ap #819-659 Semper St.", Phone = "(010) 98904612" },
                new { Id = 14, FullName = "Isaac L. Briggs", Age = 22, Address = "2690 Cubilia St.", Phone = "(036674) 688927" },
                new { Id = 15, FullName = "Brynn W. Brewer", Age = 27, Address = "P.O. Box 752, 7068 Ut Avenue", Phone = "(036124) 919078" },
                new { Id = 16, FullName = "Simon Burt", Age = 18, Address = "Ap #675-889 Ut Street", Phone = "(060) 50459644" },
                new { Id = 17, FullName = "Mason R. Avila", Age = 18, Address = "Ap #351-1512 Nisi Street", Phone = "(0488) 42259134" },
                new { Id = 18, FullName = "Abraham Sanford", Age = 29, Address = "P.O. Box 660, 3235 Tincidunt, Road", Phone = "(0515) 73476205" },
                new { Id = 19, FullName = "Kevyn Buckley", Age = 28, Address = "625-923 Neque St.", Phone = "(04424) 3003924" },
                new { Id = 20, FullName = "Donovan Boyle", Age = 20, Address = "Ap #416-1840 Donec St.", Phone = "(0026) 81130363" },
                new { Id = 21, FullName = "Grady X. Slater", Age = 24, Address = "P.O. Box 541, 8104 Enim. St.", Phone = "(0256) 22347381" },
                new { Id = 22, FullName = "Deborah X. Poole", Age = 22, Address = "Ap #843-6865 Malesuada Street", Phone = "(032593) 238373" },
                new { Id = 23, FullName = "Charity E. Bond", Age = 26, Address = "Ap #689-6045 Nibh Ave", Phone = "(07249) 1036255" },
                new { Id = 24, FullName = "Bree Hicks", Age = 23, Address = "P.O. Box 696, 6121 Cum Rd.", Phone = "(0865) 45238242" },
                new { Id = 25, FullName = "Jacqueline Howell", Age = 27, Address = "P.O. Box 330, 7585 Maecenas Av.", Phone = "(0603) 47512685" },
                new { Id = 26, FullName = "Odessa F. Dawson", Age = 19, Address = "957-7556 Hendrerit Rd.", Phone = "(091) 92558210" },
                new { Id = 27, FullName = "Joan Lang", Age = 24, Address = "822-2037 Nulla Ave", Phone = "(022) 20542136" },
                new { Id = 28, FullName = "Ira Callahan", Age = 22, Address = "115-730 Sodales St.", Phone = "(039391) 370272" },
                new { Id = 29, FullName = "Driscoll N. Garner", Age = 30, Address = "P.O. Box 392, 2051 Risus. St.", Phone = "(034451) 701302" },
                new { Id = 30, FullName = "Kermit B. Estes", Age = 19, Address = "P.O. Box 694, 1239 Consequat Rd.", Phone = "(0450) 24394797" },
                new { Id = 31, FullName = "Ciara Y. Simmons", Age = 20, Address = "Ap #883-4050 Dignissim Rd.", Phone = "(038414) 990647" },
                new { Id = 32, FullName = "Hasad Gamble", Age = 30, Address = "P.O. Box 162, 2096 Quis Street", Phone = "(0573) 30100416" },
                new { Id = 33, FullName = "Alice I. Mckenzie", Age = 21, Address = "Ap #956-6158 Proin Rd.", Phone = "(033233) 394108" },
                new { Id = 34, FullName = "Elizabeth P. Dickson", Age = 22, Address = "397-8448 Placerat. Road", Phone = "(044) 40146328" },
                new { Id = 35, FullName = "Neil Quinn", Age = 20, Address = "P.O. Box 486, 197 Ridiculus Street", Phone = "(033251) 329050" },
                new { Id = 36, FullName = "Daphne Y. Mckay", Age = 24, Address = "899-4118 Gravida Rd.", Phone = "(0225) 46452181" },
                new { Id = 37, FullName = "Ralph W. Steele", Age = 18, Address = "P.O. Box 646, 7990 Fusce Ave", Phone = "(038411) 556609" },
                new { Id = 38, FullName = "Adrienne Gonzalez", Age = 23, Address = "P.O. Box 845, 7179 Non, Rd.", Phone = "(061) 27473791" },
                new { Id = 39, FullName = "Barclay U. Head", Age = 26, Address = "8014 Nullam Avenue", Phone = "(09788) 0572425" },
                new { Id = 40, FullName = "Kiayada Bartlett", Age = 18, Address = "P.O. Box 177, 2348 Egestas Road", Phone = "(0042) 38330657" },
                new { Id = 41, FullName = "Alan D. Vaughn", Age = 25, Address = "Ap #324-2318 Elit, Ave", Phone = "(035544) 655897" },
                new { Id = 42, FullName = "Brianna Kaufman", Age = 25, Address = "8267 Primis Rd.", Phone = "(0736) 52167357" },
                new { Id = 43, FullName = "Rhonda Bass", Age = 22, Address = "9136 Diam Avenue", Phone = "(036662) 533428" },
                new { Id = 44, FullName = "Dahlia G. Mcbride", Age = 26, Address = "Ap #462-9853 Consectetuer Street", Phone = "(050) 71559178" },
                new { Id = 45, FullName = "Howard K. Franklin", Age = 26, Address = "1428 Cursus Street", Phone = "(01221) 8813050" },
                new { Id = 46, FullName = "Quin K. Fisher", Age = 25, Address = "131-6581 Urna Road", Phone = "(036652) 041044" },
                new { Id = 47, FullName = "Lilah Ellis", Age = 18, Address = "P.O. Box 719, 9496 Nec, St.", Phone = "(030612) 679027" },
                new { Id = 48, FullName = "Michael R. Lindsey", Age = 20, Address = "3860 Etiam St.", Phone = "(0786) 41008679" },
                new { Id = 49, FullName = "Ivor L. William", Age = 26, Address = "631-1995 Massa. Rd.", Phone = "(032813) 434706" },
                new { Id = 50, FullName = "Dakota Wolf", Age = 28, Address = "Ap #681-7811 At, Rd.", Phone = "(02463) 8097354" },
                new { Id = 51, FullName = "Felicia E. George", Age = 23, Address = "524-1957 A Road", Phone = "(033169) 607604" },
                new { Id = 52, FullName = "Barbara M. Melton", Age = 28, Address = "6292 Quam Road", Phone = "(07479) 8185744" },
                new { Id = 53, FullName = "Ruth Figueroa", Age = 26, Address = "288-7392 Ipsum Ave", Phone = "(001) 28336651" },
                new { Id = 54, FullName = "Dieter Douglas", Age = 22, Address = "P.O. Box 170, 3060 Ultrices. Road", Phone = "(031804) 655952" },
                new { Id = 55, FullName = "Castor Holder", Age = 23, Address = "3491 Ultricies Road", Phone = "(055) 57504980" },
                new { Id = 56, FullName = "Meghan X. Wynn", Age = 30, Address = "Ap #615-6233 Eu Road", Phone = "(0335) 82161509" },
                new { Id = 57, FullName = "Cyrus S. Abbott", Age = 19, Address = "Ap #290-6048 Cubilia Avenue", Phone = "(090) 72281571" },
                new { Id = 58, FullName = "Kimberly H. Donaldson", Age = 21, Address = "452-3010 Neque Street", Phone = "(026) 08427303" },
                new { Id = 59, FullName = "Karleigh B. Livingston", Age = 29, Address = "P.O. Box 817, 4413 Nullam Av.", Phone = "(08311) 5989306" },
                new { Id = 60, FullName = "Theodore Fitzgerald", Age = 30, Address = "857-1266 Nisl Street", Phone = "(0572) 61532694" },
                new { Id = 61, FullName = "Ayanna V. Mcmillan", Age = 30, Address = "P.O. Box 288, 1875 Nunc Ave", Phone = "(06208) 9487111" },
                new { Id = 62, FullName = "Palmer P. Guzman", Age = 29, Address = "Ap #709-7478 Fringilla Rd.", Phone = "(072) 97204208" },
                new { Id = 63, FullName = "Noelani Finch", Age = 18, Address = "P.O. Box 954, 1592 Vitae Avenue", Phone = "(00797) 1621373" },
                new { Id = 64, FullName = "Galvin Osborn", Age = 28, Address = "132-3358 Mauris St.", Phone = "(001) 55149533" },
                new { Id = 65, FullName = "Jakeem F. Lane", Age = 21, Address = "4429 Elit Avenue", Phone = "(01949) 2399757" },
                new { Id = 66, FullName = "Plato Payne", Age = 28, Address = "572-1206 Tristique St.", Phone = "(025) 34951826" },
                new { Id = 67, FullName = "Emmanuel Lynn", Age = 25, Address = "Ap #523-7533 Tristique Road", Phone = "(030204) 313297" },
                new { Id = 68, FullName = "Ahmed M. Workman", Age = 22, Address = "691-5673 Curabitur Road", Phone = "(074) 79607170" },
                new { Id = 69, FullName = "Jenna X. Cain", Age = 24, Address = "902-6972 Feugiat St.", Phone = "(035911) 460192" },
                new { Id = 70, FullName = "Veda J. Lindsay", Age = 19, Address = "620 Cursus, Avenue", Phone = "(0473) 21296423" },
                new { Id = 71, FullName = "Donna Woodward", Age = 28, Address = "515-1080 Et Avenue", Phone = "(012) 52501714" },
                new { Id = 72, FullName = "Price Grant", Age = 30, Address = "Ap #708-6948 Ante Ave", Phone = "(01773) 2688743" },
                new { Id = 73, FullName = "Rowan Rios", Age = 28, Address = "369-813 Feugiat. Rd.", Phone = "(039368) 505528" },
                new { Id = 74, FullName = "Lysandra U. Jones", Age = 28, Address = "Ap #740-319 Proin Avenue", Phone = "(066) 29320456" },
                new { Id = 75, FullName = "Gisela Ortega", Age = 26, Address = "P.O. Box 706, 2647 Nam Rd.", Phone = "(0062) 27680382" },
                new { Id = 76, FullName = "Ferdinand Spencer", Age = 18, Address = "P.O. Box 328, 3011 Adipiscing Road", Phone = "(030961) 630646" },
                new { Id = 77, FullName = "Noelle W. Santiago", Age = 26, Address = "374-7007 Eu Avenue", Phone = "(087) 82821929" },
                new { Id = 78, FullName = "Melanie U. Lambert", Age = 18, Address = "515-3258 Phasellus Street", Phone = "(0435) 83597756" },
                new { Id = 79, FullName = "Burton E. Kaufman", Age = 22, Address = "429-5718 Gravida Ave", Phone = "(00411) 5330068" },
                new { Id = 80, FullName = "Nevada X. Frederick", Age = 26, Address = "Ap #931-3912 Lacus, Rd.", Phone = "(00738) 3925747" },
                new { Id = 81, FullName = "Ima R. Santana", Age = 19, Address = "Ap #909-1234 Elit Avenue", Phone = "(044) 55128034" },
                new { Id = 82, FullName = "Ulysses Tyson", Age = 22, Address = "874-1601 Aliquam St.", Phone = "(03329) 7852326" },
                new { Id = 83, FullName = "May Joyner", Age = 23, Address = "7895 Urna. St.", Phone = "(0062) 16762838" },
                new { Id = 84, FullName = "Jakeem Holland", Age = 24, Address = "7485 Turpis Avenue", Phone = "(018) 41925211" },
                new { Id = 85, FullName = "Kitra T. Haynes", Age = 23, Address = "Ap #106-6444 Mauris Rd.", Phone = "(035436) 907316" },
                new { Id = 86, FullName = "Tanya Sampson", Age = 22, Address = "8535 Inceptos Rd.", Phone = "(07503) 3298471" },
                new { Id = 87, FullName = "Francis Olson", Age = 23, Address = "113-4133 Fringilla Avenue", Phone = "(0287) 58732930" },
                new { Id = 88, FullName = "Sophia Wise", Age = 22, Address = "P.O. Box 276, 5101 Felis. Street", Phone = "(034930) 219416" },
                new { Id = 89, FullName = "Halla Soto", Age = 27, Address = "P.O. Box 263, 6542 Risus. Street", Phone = "(038347) 925068" },
                new { Id = 90, FullName = "Bruce Dyer", Age = 28, Address = "838-4317 Et Avenue", Phone = "(07396) 9531508" },
                new { Id = 91, FullName = "Quin Carver", Age = 21, Address = "P.O. Box 450, 418 Et St.", Phone = "(0656) 44248068" },
                new { Id = 92, FullName = "Wade Lynch", Age = 24, Address = "808-2319 Ut Street", Phone = "(0952) 22992282" },
                new { Id = 93, FullName = "Josiah W. Waller", Age = 27, Address = "Ap #969-4544 Pede. Avenue", Phone = "(038979) 643360" },
                new { Id = 94, FullName = "Trevor J. Wheeler", Age = 19, Address = "715-3500 Arcu. Av.", Phone = "(070) 89818731" },
                new { Id = 95, FullName = "Stacey Mcmillan", Age = 28, Address = "Ap #563-3440 Molestie St.", Phone = "(035252) 836785" },
                new { Id = 96, FullName = "Chava Moon", Age = 21, Address = "Ap #884-7686 Blandit Rd.", Phone = "(032891) 199166" },
                new { Id = 97, FullName = "Cecilia Joseph", Age = 26, Address = "P.O. Box 854, 8764 A Avenue", Phone = "(030830) 324640" },
                new { Id = 98, FullName = "Reuben Bryan", Age = 21, Address = "754-6216 Sodales Av.", Phone = "(080) 95044686" },
                new { Id = 99, FullName = "Barclay D. Pace", Age = 30, Address = "Ap #724-9249 Phasellus Street", Phone = "(0318) 50528490" },
                new { Id = 100, FullName = "Prescott Albert", Age = 22, Address = "3552 Pretium St.", Phone = "(030786) 058443" }
            };

            var customers = new[] {
                new { Id = 1, FullName = "Quin Burgess", Age = 27, Address = "516-4029 Tincidunt St.", Phone = "(037748) 759921" },
                new { Id = 2, FullName = "Reuben Burch", Age = 26, Address = "938-9828 Et Rd.", Phone = "(00552) 6915990" },
                new { Id = 3, FullName = "Rafael Q. Burris", Age = 25, Address = "2069 Enim. Rd.", Phone = "(0481) 44137231" },
                new { Id = 4, FullName = "Lois S. Pena", Age = 18, Address = "Ap #197-5629 Gravida Av.", Phone = "(00065) 8659667" },
                new { Id = 5, FullName = "Emi K. Bowman", Age = 22, Address = "250 Enim, Ave", Phone = "(0683) 86447156" },
                new { Id = 6, FullName = "Sage Y. Price", Age = 27, Address = "194-1740 Sociis Av.", Phone = "(093) 04402343" },
                new { Id = 7, FullName = "Lavinia Dalton", Age = 18, Address = "P.O. Box 657, 6834 Vitae Rd.", Phone = "(0856) 79243614" },
                new { Id = 8, FullName = "Denise Jacobson", Age = 24, Address = "P.O. Box 367, 738 Odio. Avenue", Phone = "(081) 77264874" },
                new { Id = 9, FullName = "Kaden York", Age = 23, Address = "P.O. Box 275, 6088 Vel Road", Phone = "(08501) 7993718" },
                new { Id = 10, FullName = "Cecilia J. Bennett", Age = 24, Address = "978-8163 Sed Rd.", Phone = "(016) 38431760" },
                new { Id = 11, FullName = "Taylor C. Sims", Age = 18, Address = "Ap #316-1685 Aenean Av.", Phone = "(05516) 4092482" },
                new { Id = 12, FullName = "Megan Z. Gutierrez", Age = 18, Address = "579-7216 Dolor Rd.", Phone = "(037748) 597937" },
                new { Id = 13, FullName = "Tad Z. Shaw", Age = 24, Address = "735-2409 Porttitor Street", Phone = "(06227) 0071764" },
                new { Id = 14, FullName = "Haley Grant", Age = 18, Address = "Ap #664-2515 Scelerisque St.", Phone = "(0521) 92293494" },
                new { Id = 15, FullName = "Martena F. Patrick", Age = 29, Address = "Ap #578-6615 Faucibus Av.", Phone = "(055) 82931350" },
                new { Id = 16, FullName = "Francesca R. Bradley", Age = 18, Address = "3856 Tempus St.", Phone = "(0376) 58018494" },
                new { Id = 17, FullName = "Leigh Flowers", Age = 18, Address = "9504 Donec St.", Phone = "(025) 78467797" },
                new { Id = 18, FullName = "Kasper Nieves", Age = 26, Address = "2196 Elementum Rd.", Phone = "(009) 65010471" },
                new { Id = 19, FullName = "Lysandra Kemp", Age = 29, Address = "P.O. Box 308, 8907 Vehicula Road", Phone = "(037260) 706164" },
                new { Id = 20, FullName = "Dacey Garner", Age = 19, Address = "P.O. Box 333, 5550 Egestas, Road", Phone = "(056) 13785564" },
                new { Id = 21, FullName = "Luke Rodriguez", Age = 18, Address = "P.O. Box 723, 4492 Sit St.", Phone = "(0386) 21111853" },
                new { Id = 22, FullName = "Mollie Floyd", Age = 18, Address = "Ap #231-3723 Congue Road", Phone = "(030229) 835581" },
                new { Id = 23, FullName = "Jessamine H. Greene", Age = 25, Address = "6508 Mattis Avenue", Phone = "(036051) 305970" },
                new { Id = 24, FullName = "Maxwell B. Howard", Age = 24, Address = "603-544 Cras Rd.", Phone = "(047) 90747029" },
                new { Id = 25, FullName = "Carlos W. Rosa", Age = 25, Address = "Ap #369-5260 Nam St.", Phone = "(06624) 7709595" },
                new { Id = 26, FullName = "Anthony L. Velasquez", Age = 27, Address = "P.O. Box 244, 2405 Fusce St.", Phone = "(04292) 9587415" },
                new { Id = 27, FullName = "Simone E. Wallace", Age = 28, Address = "376-6806 Nisl Av.", Phone = "(0159) 49110641" },
                new { Id = 28, FullName = "Dai F. Crane", Age = 29, Address = "P.O. Box 332, 3520 Malesuada Street", Phone = "(0823) 23261928" },
                new { Id = 29, FullName = "Portia Rasmussen", Age = 27, Address = "Ap #552-3161 Blandit. St.", Phone = "(03653) 0929746" },
                new { Id = 30, FullName = "Hilary Carver", Age = 18, Address = "447-2788 Nulla Rd.", Phone = "(031917) 721441" },
                new { Id = 31, FullName = "Willa White", Age = 29, Address = "408-3870 Amet St.", Phone = "(05560) 6537522" },
                new { Id = 32, FullName = "Ann U. Lara", Age = 29, Address = "Ap #966-6243 Ut, Av.", Phone = "(031615) 100853" },
                new { Id = 33, FullName = "Beau Nicholson", Age = 28, Address = "P.O. Box 414, 8934 Elit Rd.", Phone = "(0152) 74957592" },
                new { Id = 34, FullName = "Leilani P. Donovan", Age = 21, Address = "P.O. Box 877, 971 Dapibus Rd.", Phone = "(00981) 4869231" },
                new { Id = 35, FullName = "Carly Dawson", Age = 20, Address = "Ap #157-1976 Volutpat St.", Phone = "(075) 97245280" },
                new { Id = 36, FullName = "Carol D. Sanford", Age = 29, Address = "Ap #113-8327 Sem, Street", Phone = "(049) 70888774" },
                new { Id = 37, FullName = "Avye Q. Brown", Age = 25, Address = "P.O. Box 690, 1718 Vel Rd.", Phone = "(029) 58465200" },
                new { Id = 38, FullName = "Mark Willis", Age = 23, Address = "156-9960 Vel, Avenue", Phone = "(031995) 615937" },
                new { Id = 39, FullName = "Jeremy E. Burke", Age = 27, Address = "4964 Eu Rd.", Phone = "(035222) 292725" },
                new { Id = 40, FullName = "Shafira I. Cardenas", Age = 25, Address = "1778 Ut St.", Phone = "(036851) 603617" },
                new { Id = 41, FullName = "Basil A. Hudson", Age = 24, Address = "9643 Eget, Street", Phone = "(04695) 1476722" },
                new { Id = 42, FullName = "Silas E. Compton", Age = 30, Address = "P.O. Box 629, 3268 Sem Road", Phone = "(031017) 697318" },
                new { Id = 43, FullName = "Mariam W. Sims", Age = 23, Address = "Ap #828-5730 Sed Ave", Phone = "(033581) 472136" },
                new { Id = 44, FullName = "Ray Ward", Age = 18, Address = "251-3054 Diam St.", Phone = "(0400) 03669254" },
                new { Id = 45, FullName = "Mary Klein", Age = 28, Address = "Ap #871-1554 Ac Street", Phone = "(067) 63090889" },
                new { Id = 46, FullName = "Benjamin H. Mullen", Age = 24, Address = "Ap #331-7319 Nulla Road", Phone = "(0719) 84015704" },
                new { Id = 47, FullName = "Harding Clay", Age = 19, Address = "1919 Ac Rd.", Phone = "(037002) 854225" },
                new { Id = 48, FullName = "Fletcher Black", Age = 21, Address = "737-1717 Ligula Street", Phone = "(030700) 109961" },
                new { Id = 49, FullName = "Shelly F. Snider", Age = 27, Address = "194-3726 Nec Avenue", Phone = "(0904) 12975332" },
                new { Id = 50, FullName = "Cora B. Joseph", Age = 20, Address = "P.O. Box 187, 9574 Dolor Street", Phone = "(0719) 73387617" },
                new { Id = 51, FullName = "Maya D. William", Age = 29, Address = "4399 Nisl Road", Phone = "(096) 78254064" },
                new { Id = 52, FullName = "Blaze Y. Henson", Age = 27, Address = "P.O. Box 219, 3496 Quam. Rd.", Phone = "(0686) 39779853" },
                new { Id = 53, FullName = "Brielle C. Harding", Age = 28, Address = "286-4833 Nunc Rd.", Phone = "(033668) 144743" },
                new { Id = 54, FullName = "Zahir Macias", Age = 19, Address = "P.O. Box 630, 4983 Vitae Avenue", Phone = "(0259) 77143204" },
                new { Id = 55, FullName = "Melanie T. Mercer", Age = 25, Address = "Ap #948-8883 Sed Road", Phone = "(032186) 393504" },
                new { Id = 56, FullName = "Alexa Craig", Age = 20, Address = "Ap #180-6682 Ligula. Av.", Phone = "(04216) 6154816" },
                new { Id = 57, FullName = "Rogan K. Peters", Age = 19, Address = "P.O. Box 575, 3254 Fames Street", Phone = "(030107) 501846" },
                new { Id = 58, FullName = "Wang Oconnor", Age = 30, Address = "435-9615 Libero Road", Phone = "(052) 95543575" },
                new { Id = 59, FullName = "Penelope L. Shaffer", Age = 23, Address = "1554 Et Rd.", Phone = "(09051) 2649091" },
                new { Id = 60, FullName = "Xandra Barrett", Age = 22, Address = "P.O. Box 347, 8589 Faucibus Rd.", Phone = "(095) 38433058" },
                new { Id = 61, FullName = "Maxwell Espinoza", Age = 28, Address = "1934 Sociis Ave", Phone = "(07606) 2303994" },
                new { Id = 62, FullName = "Adena Terrell", Age = 24, Address = "9666 Turpis. Road", Phone = "(090) 91965706" },
                new { Id = 63, FullName = "Donna Miles", Age = 24, Address = "157-1769 Natoque Rd.", Phone = "(0635) 90649721" },
                new { Id = 64, FullName = "Rahim Austin", Age = 21, Address = "974-8231 Sit St.", Phone = "(070) 75643173" },
                new { Id = 65, FullName = "Phillip B. Walker", Age = 29, Address = "542-8431 Quisque St.", Phone = "(04065) 7831454" },
                new { Id = 66, FullName = "Griffin Allen", Age = 21, Address = "Ap #722-2515 In Rd.", Phone = "(03026) 8652320" },
                new { Id = 67, FullName = "Ciaran Bailey", Age = 27, Address = "402-6220 Ante Av.", Phone = "(035643) 795892" },
                new { Id = 68, FullName = "Sylvia Camacho", Age = 25, Address = "128-3954 Donec Road", Phone = "(0620) 39517237" },
                new { Id = 69, FullName = "Zelda X. Kemp", Age = 22, Address = "964-9485 Justo. St.", Phone = "(0478) 25293567" },
                new { Id = 70, FullName = "Camilla Lester", Age = 21, Address = "681-6261 Aliquet Street", Phone = "(034) 04880388" },
                new { Id = 71, FullName = "Jorden U. Bishop", Age = 21, Address = "Ap #445-2582 Massa. St.", Phone = "(09663) 1410658" },
                new { Id = 72, FullName = "Chava A. Austin", Age = 25, Address = "5711 Gravida Rd.", Phone = "(038436) 438800" },
                new { Id = 73, FullName = "Dylan Donovan", Age = 18, Address = "4576 Risus. St.", Phone = "(093) 90433140" },
                new { Id = 74, FullName = "Katelyn Snyder", Age = 22, Address = "P.O. Box 431, 6382 Fusce Ave", Phone = "(00402) 1572370" },
                new { Id = 75, FullName = "Paki K. Hobbs", Age = 26, Address = "Ap #124-4918 Velit Ave", Phone = "(06497) 2095440" },
                new { Id = 76, FullName = "Adam Lindsay", Age = 30, Address = "P.O. Box 785, 5627 Malesuada St.", Phone = "(0825) 18226148" },
                new { Id = 77, FullName = "Kevyn Guerrero", Age = 25, Address = "Ap #449-6047 Nulla Street", Phone = "(037919) 871186" },
                new { Id = 78, FullName = "Christine Garrison", Age = 28, Address = "1294 Congue, Av.", Phone = "(07242) 4985420" },
                new { Id = 79, FullName = "Imogene B. Johnson", Age = 23, Address = "9428 Praesent Avenue", Phone = "(06608) 5122603" },
                new { Id = 80, FullName = "Lydia Pacheco", Age = 29, Address = "725-6352 Morbi Ave", Phone = "(06797) 5727900" },
                new { Id = 81, FullName = "Raphael W. Collier", Age = 25, Address = "P.O. Box 983, 4153 Nibh Av.", Phone = "(0017) 66341507" },
                new { Id = 82, FullName = "Beau Harris", Age = 22, Address = "Ap #565-2236 Curabitur Ave", Phone = "(063) 77531551" },
                new { Id = 83, FullName = "Hunter K. Preston", Age = 29, Address = "Ap #937-9967 Dignissim Av.", Phone = "(0517) 20870955" },
                new { Id = 84, FullName = "Moses Anderson", Age = 19, Address = "P.O. Box 310, 9656 Ligula Av.", Phone = "(0595) 97127578" },
                new { Id = 85, FullName = "Daria I. Carson", Age = 30, Address = "9356 Leo. Av.", Phone = "(030495) 508797" },
                new { Id = 86, FullName = "Bradley D. Owens", Age = 19, Address = "1067 Eget Av.", Phone = "(044) 79681350" },
                new { Id = 87, FullName = "Colette Z. Schneider", Age = 27, Address = "Ap #206-4536 Ante Ave", Phone = "(086) 03100250" },
                new { Id = 88, FullName = "Maya Delgado", Age = 25, Address = "2687 Rhoncus. Av.", Phone = "(01089) 7374931" },
                new { Id = 89, FullName = "Chancellor V. Ayala", Age = 26, Address = "8822 Ut, Street", Phone = "(038) 45999134" },
                new { Id = 90, FullName = "Cara Mcdaniel", Age = 29, Address = "7922 Commodo Ave", Phone = "(03338) 1106168" },
                new { Id = 91, FullName = "Zia Mercado", Age = 23, Address = "595-5683 Auctor Ave", Phone = "(0656) 55956993" },
                new { Id = 92, FullName = "Amela T. Poole", Age = 24, Address = "Ap #876-1961 Nunc Ave", Phone = "(065) 23388051" },
                new { Id = 93, FullName = "Malik Garza", Age = 30, Address = "809-8422 Velit Ave", Phone = "(0328) 24978265" },
                new { Id = 94, FullName = "Jerome Tyson", Age = 29, Address = "Ap #563-3005 Aenean Av.", Phone = "(030944) 526232" },
                new { Id = 95, FullName = "Marshall Mcdonald", Age = 26, Address = "385-7357 Sem St.", Phone = "(08024) 1783247" },
                new { Id = 96, FullName = "Jayme Ware", Age = 24, Address = "8495 Quis Rd.", Phone = "(049) 10239483" },
                new { Id = 97, FullName = "Charles R. Ball", Age = 24, Address = "988-4506 Odio. Rd.", Phone = "(032862) 581489" },
                new { Id = 98, FullName = "Lamar V. Levy", Age = 24, Address = "949-5342 Nulla. St.", Phone = "(00977) 8000622" },
                new { Id = 99, FullName = "Leroy N. Pruitt", Age = 26, Address = "Ap #286-584 Lectus St.", Phone = "(0659) 53168810" },
                new { Id = 100, FullName = "Hiram Z. Sawyer", Age = 21, Address = "Ap #295-3538 Fringilla St.", Phone = "(04652) 0617315" }
            };

            #region Customers
            DataTable customer = new DataTable("Customers");
            DataColumn customerId = new DataColumn("Id");
            customerId.DataType = typeof(int);
            customerId.AllowDBNull = false;
            customerId.AutoIncrement = true;
            customerId.AutoIncrementSeed = 1;
            customerId.AutoIncrementStep = 1;

            DataColumn customerFullName = new DataColumn("FullName");
            customerFullName.DataType = typeof(string);

            DataColumn customerAge = new DataColumn("Age");
            customerAge.DataType = typeof(int);

            DataColumn customerAddress = new DataColumn("Address");
            customerAddress.DataType = typeof(string);

            DataColumn customerPhone = new DataColumn("Phone");
            customerPhone.DataType = typeof(string);

            customer.Columns.Add(customerId);
            customer.Columns.Add(customerFullName);
            customer.Columns.Add(customerAge);
            customer.Columns.Add(customerAddress);
            customer.Columns.Add(customerPhone);

            // customer.PrimaryKey = customerId;

            dataSet.Tables.Add(customer);
            #endregion

            #region Employees
            DataTable employee = new DataTable("Employees");
            DataColumn employeeId = new DataColumn("Id");
            employeeId.DataType = typeof(int);
            employeeId.AllowDBNull = false;
            employeeId.AutoIncrement = true;
            employeeId.AutoIncrementSeed = 1;
            employeeId.AutoIncrementStep = 1;

            DataColumn employeeFullName = new DataColumn("FullName");
            employeeFullName.DataType = typeof(string);

            DataColumn employeeAge = new DataColumn("Age");
            employeeAge.DataType = typeof(int);

            DataColumn employeeAddress = new DataColumn("Address");
            employeeAddress.DataType = typeof(string);

            DataColumn employeePhone = new DataColumn("Phone");
            employeePhone.DataType = typeof(string);

            employee.Columns.Add(employeeId);
            employee.Columns.Add(employeeFullName);
            employee.Columns.Add(employeeAge);
            employee.Columns.Add(employeeAddress);
            employee.Columns.Add(employeePhone);

            //            employee.PrimaryKey = employeeId.;

            dataSet.Tables.Add(employee);
            #endregion

            #region Carts
            DataTable cart = new DataTable("Carts");
            DataColumn cartId = new DataColumn("Id");
            cartId.DataType = typeof(int);
            cartId.AllowDBNull = false;
            cartId.AutoIncrement = true;
            cartId.AutoIncrementSeed = 1;
            cartId.AutoIncrementStep = 1;

            DataColumn cartCustomerId = new DataColumn("CustomerId");
            cartCustomerId.DataType = typeof(int);

            DataColumn cartTotalSum = new DataColumn("TotalSum");
            cartTotalSum.DataType = typeof(int);

            cart.Columns.Add(cartId);
            cart.Columns.Add(cartCustomerId);
            cart.Columns.Add(cartTotalSum);

            dataSet.Tables.Add(cart);

            // cart.PrimaryKey = cartId;
            DataRelation cartCustomerIdRelation = new DataRelation("FK_cart_CustomerId", customerId, cartCustomerId);
            #endregion

            #region OrderStatus
            DataTable orderStatus = new DataTable("OrderStatus");
            DataColumn orderStatusId = new DataColumn("Id");
            orderStatusId.DataType = typeof(int);
            orderStatusId.AllowDBNull = false;
            orderStatusId.AutoIncrement = true;
            orderStatusId.AutoIncrementSeed = 1;
            orderStatusId.AutoIncrementStep = 1;

            DataColumn orderStatusName = new DataColumn("OrderStatusName");
            orderStatusName.DataType = typeof(string);

            orderStatus.Columns.Add(orderStatusId);
            orderStatus.Columns.Add(orderStatusName);
            dataSet.Tables.Add(orderStatus);
            #endregion

            #region Orders
            DataTable order = new DataTable("Orders");
            DataColumn orderId = new DataColumn("Id");
            orderId.DataType = typeof(int);
            orderId.AllowDBNull = false;
            orderId.AutoIncrement = true;
            orderId.AutoIncrementSeed = 1;
            orderId.AutoIncrementStep = 1;

            DataColumn orderCustomerId = new DataColumn("CustomerId");
            orderCustomerId.DataType = typeof(int);

            DataColumn orderEmployeeId = new DataColumn("EmployeeId");
            orderEmployeeId.DataType = typeof(int);

            DataColumn orderTotalSum = new DataColumn("TotalSum");
            orderTotalSum.DataType = typeof(int);

            DataColumn orderDate = new DataColumn("OrderDate");
            orderDate.DataType = typeof(DateTime);

            DataColumn orderOrderStatusId = new DataColumn("OrderStatusId");
            orderOrderStatusId.DataType = typeof(int);

            order.Columns.Add(orderId);
            order.Columns.Add(orderCustomerId);
            order.Columns.Add(orderEmployeeId);
            order.Columns.Add(orderTotalSum);
            order.Columns.Add(orderDate);
            order.Columns.Add(orderOrderStatusId);

            dataSet.Tables.Add(order);

            DataRelation orderCustomerIdRelation = new DataRelation("FK_order_CustomerId", customerId, orderCustomerId);
            DataRelation orderEmployeeIdRelation = new DataRelation("FK_order_EmployeeId", employeeId, orderEmployeeId);
            DataRelation orderOrderStatusIdRelation = new DataRelation("FK_order_orderStatusId", orderStatusId, orderOrderStatusId);
            #endregion

            #region Manufacturers
            DataTable manufacturer = new DataTable("Manufacturers");
            DataColumn manufacturerId = new DataColumn("Id");
            manufacturerId.DataType = typeof(int);
            manufacturerId.AllowDBNull = false;
            manufacturerId.AutoIncrement = true;
            manufacturerId.AutoIncrementSeed = 1;
            manufacturerId.AutoIncrementStep = 1;

            DataColumn manufacturerName = new DataColumn("Name");
            manufacturerName.DataType = typeof(string);

            manufacturer.Columns.Add(manufacturerId);
            manufacturer.Columns.Add(manufacturerName);
            dataSet.Tables.Add(manufacturer);
            #endregion

            #region Categories
            DataTable category = new DataTable("Categories");
            DataColumn categoryId = new DataColumn("Id");
            categoryId.DataType = typeof(int);
            categoryId.AllowDBNull = false;
            categoryId.AutoIncrement = true;
            categoryId.AutoIncrementSeed = 1;
            categoryId.AutoIncrementStep = 1;

            DataColumn categoryName = new DataColumn("Name");
            categoryName.DataType = typeof(string);

            category.Columns.Add(categoryId);
            category.Columns.Add(categoryName);
            dataSet.Tables.Add(category);
            #endregion

            #region Regregerators
            DataTable good = new DataTable("Regregerators");
            DataColumn goodId = new DataColumn("Id");
            goodId.DataType = typeof(int);
            goodId.AllowDBNull = false;
            goodId.AutoIncrement = true;
            goodId.AutoIncrementSeed = 1;
            goodId.AutoIncrementStep = 1;

            DataColumn goodName = new DataColumn("Name");
            goodName.DataType = typeof(string);

            DataColumn goodManufacturerId = new DataColumn("ManufacturerId");
            goodManufacturerId.DataType = typeof(int);

            DataColumn goodCategoryId = new DataColumn("CategoryId");
            goodCategoryId.DataType = typeof(int);

            DataColumn goodDescription = new DataColumn("Description");
            goodDescription.DataType = typeof(string);

            DataColumn goodPrice = new DataColumn("Price");
            goodPrice.DataType = typeof(int);

            good.Columns.Add(goodId);
            good.Columns.Add(goodName);
            good.Columns.Add(goodManufacturerId);
            good.Columns.Add(goodCategoryId);
            good.Columns.Add(goodDescription);
            good.Columns.Add(goodPrice);

            dataSet.Tables.Add(good);

            DataRelation goodManufacturerIdRelation = new DataRelation("FK_good_ManufacturerId", manufacturerId, goodManufacturerId);
            DataRelation goodCategoryIdRelation = new DataRelation("FK_good_CategoryId", categoryId, goodCategoryId);
            #endregion

            #region CartGood
            DataTable cartGood = new DataTable("CartGood");
            DataColumn cartGoodId = new DataColumn("Id");
            cartGoodId.DataType = typeof(int);
            cartGoodId.AllowDBNull = false;
            cartGoodId.AutoIncrement = true;
            cartGoodId.AutoIncrementSeed = 1;
            cartGoodId.AutoIncrementStep = 1;

            DataColumn cartGoodCartId = new DataColumn("CartId");
            cartGoodCartId.DataType = typeof(int);

            DataColumn cartGoodGoodId = new DataColumn("GoodId");
            cartGoodGoodId.DataType = typeof(int);

            DataColumn cartGoodGoodCount = new DataColumn("GoodCount");
            cartGoodGoodCount.DataType = typeof(int);

            cartGood.Columns.Add(cartGoodId);
            cartGood.Columns.Add(cartGoodCartId);
            cartGood.Columns.Add(cartGoodGoodId);
            cartGood.Columns.Add(cartGoodGoodCount);

            dataSet.Tables.Add(cartGood);

            DataRelation cartGoodCartIdRelation = new DataRelation("FK_cartGood_CartId", cartId, cartGoodCartId);
            DataRelation cartGoodGoodIdRelation = new DataRelation("FK_cartGood_GoodId", goodId, cartGoodGoodId);
            #endregion

            #region OrderGood
            DataTable orderGood = new DataTable("OrderGood");
            DataColumn orderGoodId = new DataColumn("Id");
            orderGoodId.DataType = typeof(int);
            orderGoodId.AllowDBNull = false;
            orderGoodId.AutoIncrement = true;
            orderGoodId.AutoIncrementSeed = 1;
            orderGoodId.AutoIncrementStep = 1;

            DataColumn orderGoodOrderId = new DataColumn("OrderId");
            orderGoodOrderId.DataType = typeof(int);

            DataColumn orderGoodGoodId = new DataColumn("GoodId");
            orderGoodGoodId.DataType = typeof(int);

            DataColumn orderGoodGoodCount = new DataColumn("GoodCount");
            orderGoodGoodCount.DataType = typeof(int);

            orderGood.Columns.Add(orderGoodId);
            orderGood.Columns.Add(orderGoodOrderId);
            orderGood.Columns.Add(orderGoodGoodId);
            orderGood.Columns.Add(orderGoodGoodCount);

            dataSet.Tables.Add(orderGood);

            DataRelation orderGoodCartIdRelation = new DataRelation("FK_orderGood_CartId", orderId, orderGoodOrderId);
            DataRelation orderGoodGoodIdRelation = new DataRelation("FK_orderGood_CartId", goodId, orderGoodGoodId);
            #endregion

            foreach (var item in customers)
                customer.Rows.Add(new object[] { item.Id, item.FullName, item.Age, item.Address, item.Phone });

            foreach (var item in employees)
                employee.Rows.Add(new object[] { item.Id, item.FullName, item.Age, item.Address, item.Phone });

            foreach (var item in categories)
                category.Rows.Add(new object[] { item.Id, item.Name });

            foreach (var item in manufacturers)
                category.Rows.Add(new object[] { item.Id, item.Name });

            foreach (var item in goods)
                good.Rows.Add(new object[] { item.Id, item.Name, item.ManufacturerId, item.CategoryId, item.Description, item.Price });

            return dataSet;
        }

        //Метод удаления товара из Корзины с учетом Id клиента
        static void DeleteCartGood(DataSet dataSet, int customersId)
        {
            string goodIdAsString = "";
            while (!int.TryParse(goodIdAsString, out int goodId))
            {
                Console.WriteLine("Введите Id товара, которое хотите удалить: ");
                goodIdAsString = Console.ReadLine();
                GetCartGoodRowsCollections(dataSet, customersId, out List<int> goodsIds, out List<int> goodsCount);

                foreach (DataTable dt in dataSet.Tables)
                {
                    if (dt.TableName == "CartGood")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if ((int)row.ItemArray[1] == customersId)
                            {
                                for (int i = 0; i < goodsIds.Count; i++)
                                {
                                    if ((int)row.ItemArray[2] == goodsIds[i])
                                    {
                                        foreach (DataTable goodDataTable in dataSet.Tables)
                                        {
                                            if (goodDataTable.TableName == "Regregerators")
                                            {
                                                foreach (DataRow goodDataRow in goodDataTable.Rows)
                                                {
                                                    for (int j = 0; j < goodsIds.Count; j++)
                                                    {
                                                        if (goodsIds[j] == (int)goodDataRow.ItemArray[0])
                                                        {
                                                            Console.WriteLine($"Товар: {goodDataRow.ItemArray[1]} был успешно удален.");
                                                            row.Delete();
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Этого холодильника нет в списке!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Метод выбора способа оплаты
        static void PickPaymentMethod()
        {

        }

        //Метод Оплаты заказа
        static void PayOrder(DataSet dataSet, int orderId)
        {
            GetOrderGoodRowsCollections(dataSet, orderId, out List<int> goodsIds, out List<int> goodsCount);
            GetGoodsPriceCollection(dataSet, goodsIds, out List<int> goodsPrice);

            List<int> sum = new List<int>();
            int commonSum = 0;
            //Устанаваливаем общую сумму
            for (int i = 0; i < goodsIds.Count; i++)
            {
                sum.Add(goodsCount[i] * goodsPrice[i]);
                commonSum += sum[i];
            }
            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Regregerators")
                {
                    foreach (DataRow datarow in dt.Rows)
                    {
                        for (int i = 0; i < goodsIds.Count; i++)
                        {
                            if (goodsIds[i] == (int)datarow.ItemArray[0])
                            {
                                Console.WriteLine($"\n\tНазвание товара: {datarow.ItemArray[1]}\n\tЦена: {datarow.ItemArray[5]} x {goodsCount[i]} = {((int)datarow.ItemArray[5] * goodsCount[i])}");
                            }
                        }
                    }
                }
            }

            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Orders")
                {
                    foreach (DataRow datarow in dt.Rows)
                    {
                        if (orderId == (int)datarow.ItemArray[0])
                        {
                            datarow.ItemArray[3] = commonSum;
                            datarow.AcceptChanges();
                            Console.WriteLine($"\n\t - - - Вы оплатили ${commonSum} - - -");
                            Console.WriteLine("\tСпасибо за покупку, до свидания!)");
                            return;
                        }
                    }
                }
            }
        }

        //Метод удаления товара из Orders с учетом Id
        static void DeleteOrderGood(DataSet dataSet, int orderId)
        {
            if (IsEmptyOrderGoodTable(dataSet, orderId))
                return;

            string goodIdAsString = "";
            while (!int.TryParse(goodIdAsString, out int goodId))
            {
                GetOrderGoodRowsCollections(dataSet, orderId, out List<int> goodsIds, out List<int> goodsCount);
                Console.WriteLine("\n\t - - - Ваши товары - - -");
                foreach (DataTable goodDataTable in dataSet.Tables)
                {
                    if (goodDataTable.TableName == "Regregerators")
                    {
                        foreach (DataRow goodDataRow in goodDataTable.Rows)
                        {
                            for (int i = 0; i < goodsIds.Count; i++)
                            {
                                if (goodsIds[i] == (int)goodDataRow.ItemArray[0])
                                {
                                    Console.WriteLine($"Название товара: {goodDataRow.ItemArray[1]} Цена: {goodDataRow.ItemArray[5]} x {goodsCount[i]} = {((int)goodDataRow.ItemArray[5] * goodsCount[i])}");
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("Введите Id товара, которое хотите удалить: ");
                goodIdAsString = Console.ReadLine();

                foreach (DataTable dt in dataSet.Tables)
                {
                    if (dt.TableName == "OrderGood")
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if ((int)row.ItemArray[1] == orderId)
                            {
                                for (int i = 0; i < goodsIds.Count; i++)
                                {
                                    if ((int)row.ItemArray[2] == goodsIds[i])
                                    {
                                        foreach (DataTable goodDataTable in dataSet.Tables)
                                        {
                                            if (goodDataTable.TableName == "Regregerators")
                                            {
                                                foreach (DataRow goodDataRow in goodDataTable.Rows)
                                                {
                                                    for (int j = 0; j < goodsIds.Count; j++)
                                                    {
                                                        if (goodsIds[j] == (int)goodDataRow.ItemArray[0])
                                                        {
                                                            Console.WriteLine($"Товар: {goodDataRow.ItemArray[1]} был успешно удален.");
                                                            row.Delete();
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Этого товара нет в списке!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Метод возвращающий все поля с таблицы OrderGood которая связывает Заказ и Товары с учетом Id Клиента
        static void GetOrderGoodRowsCollections(DataSet dataSet, int orderId, out List<int> goodsIds, out List<int> goodsCount)
        {
            goodsIds = new List<int>();
            goodsCount = new List<int>();
            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "OrderGood")
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (orderId == (int)dataRow.ItemArray[1])
                        {
                            goodsIds.Add((int)dataRow.ItemArray[2]);
                            goodsCount.Add((int)dataRow.ItemArray[3]);
                        }
                    }
                }
            }
        }

        //Вывод на экран статуса заказа
        static void ShowOrderStatus(DataSet dataSet, int orderStatusId)
        {
            int employeeId = 0;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "Orders")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[0] == orderStatusId)
                        {
                            employeeId = (int)dataRow.ItemArray[2];//Сохраняем Id сотрудника
                        }
                    }
                }
            }

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "OrderStatus")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[0] == orderStatusId)
                        {
                            foreach (DataTable employeeTable in dataSet.Tables)
                            {
                                if (employeeTable.TableName == "Employees")
                                {
                                    foreach (DataRow employeeRow in employeeTable.Rows)
                                    {
                                        if ((int)employeeRow.ItemArray[0] == employeeId)
                                        {
                                            Console.WriteLine($"\n\t - - - {dataRow.ItemArray[1]} курьером {employeeRow.ItemArray[1]} tel: {employeeRow.ItemArray[4]} - - - ");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Изменение статуса заказа
        static void ChangeOrderStatus(DataSet dataSet, int orderStatusId, string statusName)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "OrderStatus")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[0] == orderStatusId)
                        {
                            //dataRow.BeginEdit();
                            dataRow.ItemArray[1] = statusName;
                            dataRow.AcceptChanges();
                            //dataRow.EndEdit();
                        }
                    }
                }
            }
        }

        //Метод выбора курьера
        static int PickEmployee()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        //Метод проверяющий наличие холодильников в корзине с учетом Id клиента
        static bool IsEmptyOrderGoodTable(DataSet dataSet, int orderId)
        {
            bool signal = true;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "OrderGood")
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        EmptyOrderMessage();
                        return true;
                    }
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[1] == orderId)
                        {
                            return false;
                        }
                        else
                        {
                            signal = false;
                        }
                    }
                    //Если сигнал не равен истине то список заказов пуст
                    if (!signal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Вывод на экран сообщения о пустом списке заказов
        static void EmptyOrderMessage()
        {
            Console.WriteLine("\n\t - - - Список заказов пуст! - - -");
            Console.WriteLine("\n\t - - - Вы оплатили за доставку 2000 тг! - - -");
        }

        //Метод очистки Orders с учетом Id, чтобы не удалить данные относящиеся к другим заказам
        static void ClearOrder(DataSet dataSet, int orderId)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "OrderGood")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[1] == orderId)
                        {
                            dataRow.Delete();
                        }
                    }
                }
            }

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "Orders")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[0] == orderId)
                        {
                            dataRow.Delete();
                        }
                    }
                }
            }
            EmptyOrderMessage();
        }

        //Метод очистки Корзины с учетом Id клиента, чтобы не удалить данные относящиеся к другим пользователям
        static void ClearCart(DataSet dataSet, int customersId)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "Carts")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[1] == customersId)
                        {
                            dataRow.ItemArray[2] = 0;
                        }
                    }
                }
            }

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "CartGood")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[1] == customersId)
                        {
                            dataRow.Delete();
                            return;
                        }
                    }
                }
            }
        }

        //Метод устанавливающий кол-во холодильников и добавляющий значения в таблицу CartGood, которая связывает Корзину с Товарами
        static void InsertToCart(DataSet dataSet, int goodsId, int customersId)
        {
            int goodCount = 0;
            string countAsString = "";
            while ((!int.TryParse(countAsString, out goodCount)) || (goodCount > 10) || (goodCount < 1))
            {
                Console.Write("Выберите кол-во: ");
                countAsString = Console.ReadLine();
                if (!int.TryParse(countAsString, out goodCount))
                    ParseError();
                if ((goodCount > 10) || (goodCount < 1))
                    IdError();
            }

            DataRow cartGoodNewRow = dataSet.Tables["CartGood"].NewRow();
            cartGoodNewRow["CartId"] = customersId;
            cartGoodNewRow["GoodId"] = goodsId;
            cartGoodNewRow["GoodCount"] = goodCount;
            dataSet.Tables["CartGood"].Rows.Add(cartGoodNewRow);

            SetCartCommonSum(dataSet, customersId);

            Console.WriteLine("\n \t --- Холодильник(и) добавлен(ы) в список --- \n");
        }

        //Метод записывающий общую сумму в Корзину
        static void SetCartCommonSum(DataSet dataSet, int customersId)
        {
            List<int> sum = new List<int>();
            int commonSum = 0;

            //Записываем все поля с Таблицы CartGood, которая связывает Корзину и Товары с учетом Id Клиента
            GetCartGoodRowsCollections(dataSet, customersId, out List<int> goodsIds, out List<int> goodsCount);

            //Записываем все цены с таблицы Regregerators, кол-во холодильников соответствует размеру коллекции
            GetGoodsPriceCollection(dataSet, goodsIds, out List<int> goodsPrice);

            //Устанаваливаем общую сумму
            for (int i = 0; i < goodsIds.Count; i++)
            {
                sum.Add(goodsCount[i] * goodsPrice[i]);
                commonSum += sum[i];
            }

            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Carts")
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (customersId == (int)dataRow.ItemArray[0])
                        {
                            dataRow.ItemArray[2] = commonSum;
                        }
                    }
                }
            }
        }

        //Метод возвращающий все поля с таблицы CartGood которая связывает Корзину и Товары с учетом Id Клиента
        static void GetCartGoodRowsCollections(DataSet dataSet, int customersId, out List<int> goodsIds, out List<int> goodsCount)
        {
            goodsIds = new List<int>();
            goodsCount = new List<int>();
            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "CartGood")
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (customersId == (int)dataRow.ItemArray[1])
                        {
                            goodsIds.Add((int)dataRow.ItemArray[2]);
                            goodsCount.Add((int)dataRow.ItemArray[3]);
                        }
                    }
                }
            }
        }

        //Метод записывающий все цены холодильников с учетом Id
        static void GetGoodsPriceCollection(DataSet dataSet, List<int> goodsIds, out List<int> goodsPrice)
        {
            goodsPrice = new List<int>();
            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Regregerators")
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        for (int i = 0; i < goodsIds.Count; i++)
                        {
                            if (goodsIds[i] == (int)dataRow.ItemArray[0])
                            {
                                goodsPrice.Add((int)dataRow.ItemArray[5]);
                            }
                        }
                    }
                }
            }
        }

        //Метод установки значений в список 
        static void SelectCustomer(DataSet dataSet, int customerId)
        {
            DataRow cartNewRow = dataSet.Tables["Carts"].NewRow();
            cartNewRow["Id"] = customerId;
            cartNewRow["CustomerId"] = customerId;
            cartNewRow["TotalSum"] = 0;
            //dataSet.Tables["Carts"].Rows.Add(new object[] { id, id, 0 });
            //--id;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "Customers")
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[0] == customerId)
                        {
                            Console.WriteLine($"\tИмя: {dataRow.ItemArray[1]}");
                            Console.WriteLine($"\tВозраст: {dataRow.ItemArray[2]}");
                            Console.WriteLine($"\tАдрес: {dataRow.ItemArray[3]}");
                            Console.WriteLine($"\tТелефон: {dataRow.ItemArray[4]}");
                        }
                    }
                }
            }
        }

        //Вывод ошибки при вводе символа
        static void ParseError()
        {
            Console.WriteLine("Введите число.");
        }

        //Вывод ошибки при выходе за пределы кол-во Id 
        static void IdError()
        {
            Console.WriteLine("Число должно быть больше 0 и меньше/равно 10.");
        }

        //Метод вывода на экран и выбора клиентов
        static int CustomersList(DataSet dataSet)
        {
            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Customers")
                {
                    Console.WriteLine(dt.TableName); // название таблицы

                    foreach (DataColumn column in dt.Columns)
                        if ((column.ColumnName == "Id") || (column.ColumnName == "FullName"))
                            Console.Write("\t\t{0}", column.ColumnName);
                    Console.WriteLine();
                    for (int i = 0; i < dt.Rows.Count / 10; i++)
                    {
                        Console.WriteLine("\t\t{0}------------{1}", dt.Rows[i].ItemArray[0], dt.Rows[i].ItemArray[1]);
                    }
                }
            }
            string customersIdAsString = "";
            int customersId = 0;
            Console.WriteLine("\n\tЗаменил ввод данных клиента выбором клиента.\n\tТо есть данные теперь не вводим [они лежат в бд], а выбираем.\n");
            while ((!int.TryParse(customersIdAsString, out customersId)) || (customersId > 10) || (customersId < 1))
            {
                Console.Write("Выберите клиента по Id: ");
                customersIdAsString = Console.ReadLine();
                if (!int.TryParse(customersIdAsString, out customersId))
                    ParseError();
                if ((customersId > 10) || (customersId < 1))
                    IdError();
            }
            Console.WriteLine("\n\t - - - Клиент был выбран - - -");
            SelectCustomer(dataSet, customersId);
            return customersId;
        }

        //Метод вывода на экран и выбора холодильников
        static void GoodsList(DataSet dataSet, int customersId)
        {
            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Regregerators")
                {
                    Console.WriteLine($"\n{dt.TableName}"); // название таблицы

                    foreach (DataColumn column in dt.Columns)
                        if ((column.ColumnName == "Id") || (column.ColumnName == "Name") || (column.ColumnName == "Price"))
                            Console.Write("\t\t{0}", column.ColumnName);
                    Console.WriteLine();
                    for (int i = 0; i < dt.Rows.Count / 10; i++)
                    {
                        Console.WriteLine("\t\t{0}\t\t{1}\t\t{2}", dt.Rows[i].ItemArray[0], dt.Rows[i].ItemArray[1], dt.Rows[i].ItemArray[5]);
                    }
                }
            }
            Console.WriteLine();
            string goodsIdAsString = "";
            int goodsId = 0;
            while (!int.TryParse(goodsIdAsString, out goodsId) || (goodsId > 10) || (goodsId < 1))
            {
                Console.Write("Выберите холодильник по Id: ");
                goodsIdAsString = Console.ReadLine();
                if (!int.TryParse(goodsIdAsString, out goodsId))
                    ParseError();
                if ((goodsId > 10) || (goodsId < 1))
                    IdError();
            }
            InsertToCart(dataSet, goodsId, customersId);
        }

        //Метод проверяющий наличие холодильников в корзине с учетом Id клиента
        static bool IsEmptyCartGoodTable(DataSet dataSet, int customersId)
        {
            bool signal = true;
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "CartGood")
                {
                    if (dataTable.Rows.Count == 0)
                    {
                        EmptyCartMessage();
                        return true;
                    }
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if ((int)dataRow.ItemArray[1] == customersId)
                        {
                            return false;
                        }
                    }
                    //Если сигнал не равен истине то корзина пуста
                    if (!signal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Вывод на экран сообщения о пустой корзине
        static void EmptyCartMessage()
        {
            Console.WriteLine("\n\t - - - Список холодильников пуст! - - -");
            Console.WriteLine("\n\t - - - Выберите минимум один - - -\n");
        }

        //Метод вывода на экран холодильников в корзине
        static void CartList(DataSet dataSet, int customersId)
        {
            Console.WriteLine("\n\t - - - Список выбранных холодильников - - -");
            if (IsEmptyCartGoodTable(dataSet, customersId))
                return;

            //Записываем все поля с Таблицы CartGood, которая связывает Корзину и Товары с учетом Id Клиента
            GetCartGoodRowsCollections(dataSet, customersId, out List<int> goodsIds, out List<int> goodsCount);

            //Записываем все цены с таблицы Regregerators, кол-во холодильников соответствует размеру коллекции
            GetGoodsPriceCollection(dataSet, goodsIds, out List<int> goodsPrice);

            foreach (DataTable dt in dataSet.Tables)
            {
                if (dt.TableName == "Regregerators")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < goodsIds.Count; j++)
                        {
                            if ((int)dt.Rows[i].ItemArray[0] == goodsIds[j])
                            {
                                Console.WriteLine($"\n\tId: {dt.Rows[i].ItemArray[0]}\n\tНазвание товара: {dt.Rows[i].ItemArray[1]}\n\tЦена: {dt.Rows[i].ItemArray[5]} x {goodsCount[j]} = {((int)dt.Rows[i].ItemArray[5] * goodsCount[j])}");
                            }
                        }
                    }
                }
            }

            string keyAsString = "";
            while (!int.TryParse(keyAsString, out int key))
            {
                Console.WriteLine("\n\t--- Меню корзины ---");
                Console.WriteLine("\tОформить заказ - 1");
                Console.WriteLine("\tВернутся в Главное меню - 2");
                Console.WriteLine("\tУдалить холодильник - 3");
                Console.WriteLine("\tОчистить список - 4");
                Console.Write("\nВведите команду: ");
                keyAsString = Console.ReadLine();
                if ((!int.TryParse(keyAsString, out key)) || (key < 1) || (key > 4))
                {
                    Console.WriteLine("\n\t --- Пожалуйста введите одну из нижеперечисленных команд ---");
                }
                else
                {
                    switch (key)
                    {
                        case 1:
                            Checkout(dataSet, customersId);
                            break;
                        case 2: break;
                        case 3:
                            DeleteCartGood(dataSet, customersId);
                            break;
                        case 4:
                            ClearCart(dataSet, customersId);
                            break;
                        default: break;
                    }
                }
            }
        }
    }
}

#region Составной Первичный ключ
// private void SetPrimaryKeys()
// {
//     // Create a new DataTable and set two DataColumn objects as primary keys.
//     DataTable table = new DataTable();
//     DataColumn[] keys = new DataColumn[2];
//     DataColumn column;

//     // Create column 1.
//     column = new DataColumn();
//     column.DataType = System.Type.GetType("System.String");
//     column.ColumnName= "FirstName";

//     // Add the column to the DataTable.Columns collection.
//     table.Columns.Add(column);

//     // Add the column to the array.
//     keys[0] = column;

//     // Create column 2 and add it to the array.
//     column = new DataColumn();
//     column.DataType = System.Type.GetType("System.String");
//     column.ColumnName = "LastName";
//     table.Columns.Add(column);

//     // Add the column to the array.
//     keys[1] = column;

//     // Set the PrimaryKeys property to the array.
//     table.PrimaryKey = keys;
// }
#endregion

#region BegindEdit
// DataRow goodsRow;
// DataRow categoriesRow;


// goodsRow = good.NewRow();
// goodsRow["Id"] = item.Id;
// goodsRow["Name"] = item.Name;
// goodsRow["ManufacturerId"] = item.ManufacturerId;
// goodsRow["CategoryId"] = item.CategoryId;
// goodsRow["Description"] = item.Description;
// goodsRow["Price"] = item.Price;
// good.Rows.Add(goodsRow);

// goodsRow.BeginEdit();
// goodsRow.ItemArray = new object[] { item.Id, "P.P.Kirsanov", 42 };
// goodsRow.EndEdit();
#endregion
