using Newtonsoft.Json;
using Server.Models;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

List<Car> cars = new List<Car>(); 

var ip = IPAddress.Parse("127.0.0.1");

var port = 2702;

var listener = new TcpListener(ip, port);

listener.Start();
string filename = "C:\\Users\\Asus\\source\\repos\\NetworkProgramingHw5\\Server\\Data\\appData.json";

while (true)
{
    var stringData = File.ReadAllText(filename);
    cars = JsonConvert.DeserializeObject<List<Car>>(stringData)!;
    Console.WriteLine("Start listinig");
    var client = listener.AcceptTcpClient();
    Console.WriteLine("connect");
    

        var clientStream = client.GetStream();
        var binaryWrite = new BinaryWriter(clientStream);
        var binaryRead = new BinaryReader(clientStream);

        while (true)
        {
            var strRead = binaryRead.ReadString();

            var command = System.Text.Json.JsonSerializer.Deserialize<Command>(strRead);

            if (command is null)
                return;

            switch (command.Method)
            {
                case HttpMethods.GET:
                    {
                        var jsonStr = System.Text.Json.JsonSerializer.Serialize(GetById(command.Car.Id));
                        binaryWrite.Write(jsonStr);
                        binaryWrite.Flush();
                        break;
                    }
                case HttpMethods.POST:
                    {
                        var jsonStr = System.Text.Json.JsonSerializer.Serialize(GetAll());
                        binaryWrite.Write(jsonStr);
                        binaryWrite.Flush();
                        break;
                    }
                case HttpMethods.PUT:
                    binaryWrite.Write(Update(command.Car));
                    binaryWrite.Flush();
                    break;
                case HttpMethods.DELETE:
                    binaryWrite.Write(Delete(command.Car.Id));
                    binaryWrite.Flush();
                    break;
                default:
                    break;
            }

        }

}


Car? GetById(int id)
{
    foreach (var car in cars)
    {
        if (car.Id == id)
        {
            return car;
        }
    }
    return new Car();
}

List<Car>? GetAll()
{
    return cars;
}

bool Add(Car car)
{
    cars.Add(car);
    return true;
}
bool Update(Car car)
{
    foreach (var item in cars)
    {
        if (item.Id == car.Id)
        {
            cars.Remove(item);
            cars.Add(car);
            return true;
        }
    }
    return false;
}
bool Delete(int id)
{
    foreach (var car in cars)
    {
        if (car.Id == id)
        {
            cars.Remove(car);
            return true;
        }
    }
    return false;
}