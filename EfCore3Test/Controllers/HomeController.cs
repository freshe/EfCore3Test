using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCore3Test.Db;
using EfCore3Test.Db.EF;
using Microsoft.AspNetCore.Mvc;

namespace EfCore3Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            await SetUpAsync();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddData()
        {
            var nodes = await _unitOfWork.NodesRepository.GetAsync();
            var data = await _unitOfWork.NodesDataRepository.GetAsync();
            var max = nodes.Max(x => x.Nr);
            var next = max + 1;

            //Add 3 new nodes
            for (var i = 0; i < 3; i++)
            {
                var newNode = new Nodes
                {
                    Nr = next + i
                };
                
                //Insert new node
                _unitOfWork.NodesRepository.Insert(newNode);

                if (i == 0)
                {
                    //Put all data in the first new node
                    foreach (var item in data)
                    {
                        item.NodeId = newNode.NodeId;
                        _unitOfWork.NodesDataRepository.Update(item);
                    }
                }
            }

            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        //Set up initial data
        public async Task SetUpAsync()
        {
            var nodes = await _unitOfWork.NodesRepository.GetAsync();
            
            if (nodes.Count == 0)
            {
                var node = new Nodes
                {
                    Nr = 1,
                    NodesData = new List<NodesData>()
                    {
                        new NodesData { Created = DateTime.UtcNow },
                        new NodesData { Created = DateTime.UtcNow }
                    }
                };
                
                _unitOfWork.NodesRepository.Insert(node);

                await _unitOfWork.SaveAsync();
            }
        }
    }
}