using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlProject.Data
{
    public interface IDataHelper<Table>
    {
        // Read
        List<Table> GetAllData();
        List<Table> GetDataByUser(string UserId);
        List<Table> Search(string SearchItem);

        Table Find(int Id);

        // Write    

        int Add(Table table);
        int Edit(int Id,Table table);

        int Delete(int Id); 
    }
}
