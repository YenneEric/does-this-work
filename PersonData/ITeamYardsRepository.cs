using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData
{
    public interface ITeamYardsRepository
    {
        IReadOnlyList<MostTeamYards> FetchMostTeamYards(int year);
    }
}