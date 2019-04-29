using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Interface
{
    public interface IFlower
    {
        void SaveFlower(Flower Flower);
        IQueryable<FlowerViewModel> ShowFlower(string sortColumn, string sortColumnDir, string Search);
        int DeleteFlower(int id);
        Flower GetFlowerByID(int id);
        void UpdateFlower(Flower Flower);
        bool CheckFlowerAlready(string FlowerName);
        List<FlowerModel> GetAllFlower();
        IEnumerable<Flower> ShowAllFlower();
    }
}
