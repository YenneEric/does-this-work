using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonData
{
    public interface IConferenceRepository
    {

        IReadOnlyList<ConferenceTeamRank> FetchConferenceTeamRank(int year, string confName);
    }
}