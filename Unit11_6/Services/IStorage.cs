using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit11_6.Models;

namespace Unit11_6.Services;
public interface IStorage
{
    Session GetSession(long chatId);
}
