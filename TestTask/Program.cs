using System.Text.Json;
using TestTask;

List<Employee> employee;

//чтение data.json
using (FileStream fs = new FileStream("data.json", FileMode.OpenOrCreate))
{
    employee = await JsonSerializer.DeserializeAsync<List<Employee>>(fs);
}

//приведение телефонных номеров к формату +7(ххх)ххх-хх-хх
foreach (var e in employee)
{
    e.PhoneNumber = string.Format("{0:+#(###)###-##-##}", Convert.ToInt64(e.PhoneNumber));
}

//сортировка по заработной плате по убыванию
var sortEmployee = employee.OrderByDescending(e => Convert.ToInt32(e.Salary));

//запись в текстовый файл 20 сотрудников с наибольшей заработной платой
using (StreamWriter writer = new StreamWriter("result.txt", false))
{
    foreach (var e in sortEmployee.Take(20))
    {
        await writer.WriteLineAsync($"{e.Name} {e.PhoneNumber} {e.Salary}");
    }
}