using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentLINQ
{
    // Định nghĩa lớp Student
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Tên: {Name}, Tuổi: {Age}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Tạo danh sách học sinh
            List<Student> students = new List<Student>()
            {
                new Student{ Id=1, Name="An", Age=16 },
                new Student{ Id=2, Name="Bình", Age=18 },
                new Student{ Id=3, Name="Anh", Age=15 },
                new Student{ Id=4, Name="Cường", Age=19 },
                new Student{ Id=5, Name="Dũng", Age=17 }
            };

            // a. In danh sách toàn bộ học sinh
            Console.WriteLine("a. Danh sách toàn bộ học sinh:");
            students.ForEach(s => Console.WriteLine(s));
            Console.WriteLine();

            // b. Học sinh có tuổi từ 15 đến 18
            Console.WriteLine("b. Học sinh có tuổi từ 15 đến 18:");
            var from15to18 = students.Where(s => s.Age >= 15 && s.Age <= 18);
            foreach (var s in from15to18) Console.WriteLine(s);
            Console.WriteLine();

            // c. Học sinh tên bắt đầu bằng 'A'
            Console.WriteLine("c. Học sinh có tên bắt đầu bằng 'A':");
            var startWithA = students.Where(s => s.Name.StartsWith("A"));
            foreach (var s in startWithA) Console.WriteLine(s);
            Console.WriteLine();

            // d. Tổng tuổi của tất cả học sinh
            Console.WriteLine("d. Tổng tuổi của tất cả học sinh:");
            int totalAge = students.Sum(s => s.Age);
            Console.WriteLine(totalAge);
            Console.WriteLine();

            // e. Học sinh có tuổi lớn nhất
            Console.WriteLine("e. Học sinh có tuổi lớn nhất:");
            int maxAge = students.Max(s => s.Age);
            var oldest = students.Where(s => s.Age == maxAge);
            foreach (var s in oldest) Console.WriteLine(s);
            Console.WriteLine();

            // f. Sắp xếp theo tuổi tăng dần
            Console.WriteLine("f. Danh sách sau khi sắp xếp theo tuổi tăng dần:");
            var sorted = students.OrderBy(s => s.Age);
            foreach (var s in sorted) Console.WriteLine(s);

            Console.ReadKey();
        }
    }
}