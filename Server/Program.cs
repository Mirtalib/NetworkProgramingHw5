using Server.Models;
using System.Net;
using System.Net.Sockets;




List<Car> cars = new List<Car>(); 

var ip = IPAddress.Parse("127.0.0.1");

var port = 27001;

var listener = new TcpListener(ip, port);

listener.Start(10);


while (true)
{
    var client = listener.AcceptTcpClient();
    var clientStream = client.GetStream();




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
    return null;
}

List<Car>? GetAll()
{
    return cars;
}
bool Add(Car car)
{
    if (car.VIN != null 
        && car.Make != null
        && car.Model != null
        && car.Year != null
        && car.Color != null)
    {
        cars.Add(car);
        return true;
    }
    else
    {
        return false;
    }


}
bool Update(Car car)
{
    return true;
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