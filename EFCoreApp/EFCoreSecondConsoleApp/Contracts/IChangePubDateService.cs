using EFCoreSecondConsoleApp.DTO;
using EFCoreSecondConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Contracts
{
    public interface IChangePubDateService
    {
        public ChangePubDateDto GetOriginal(int id);
        public Book UpdateBook(ChangePubDateDto dto);

        
    }
        
}
