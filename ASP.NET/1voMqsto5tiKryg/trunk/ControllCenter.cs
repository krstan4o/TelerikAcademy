using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SinkBreaker
{
    [HubName("sinkbreaker")]
    public class ControllCenter : Hub
    {
        DBDispatcher dbd = new DBDispatcher();
        
        public void InsertEntry(string name, int score)
        {
            dbd.HandleEntry(name.Replace(' ', '_').Replace('|', '_'), score);
            GetAllEntries();
            //Clients.Caller.alert(string.Format("{0} : {1}points has been added to the wall of fame!", name, score));
        }

        public void GetAllEntries()
        {
            List<KeyValuePair<string, int>> allEntries = dbd.GetAllEntries().ToList();
            StringBuilder sb = new StringBuilder();

            //Array.Sort(allEntries);
            //allEntries = allEntries.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToList();

            int len = allEntries.Count;
            for (int i = 0; i < len; i++)
            {
                KeyValuePair<string, int> currentEntry = allEntries[i];
                sb.Append(string.Format("{0} {1}|", currentEntry.Key, currentEntry.Value));
            }

            Clients.Caller.updateEntries(sb.ToString().TrimEnd('|'));
        }

        public void DeleteAllEntries()
        {
            dbd.ClearDB();
        }
    }
}