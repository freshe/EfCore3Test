using System.Threading.Tasks;
using EfCore3Test.Db.EF;

namespace EfCore3Test.Db
{
    public interface IUnitOfWork
    {
        Repository<Nodes> NodesRepository { get; }
        Repository<NodesData> NodesDataRepository { get; }

        Task SaveAsync();
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestDbContext _context;
        private Repository<Nodes> _nodesRepository;
        private Repository<NodesData> _nodesDataRepository;
        
        public UnitOfWork(TestDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        public Repository<Nodes> NodesRepository => _nodesRepository ??= new Repository<Nodes>(_context);
        public Repository<NodesData> NodesDataRepository => _nodesDataRepository ??= new Repository<NodesData>(_context);
    }
}