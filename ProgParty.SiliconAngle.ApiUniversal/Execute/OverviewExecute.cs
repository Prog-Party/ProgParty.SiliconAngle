using ProgParty.SiliconAngle.ApiUniversal.Result;
using ProgParty.SiliconAngle.ApiUniversal.Scrape;
using System.Collections.Generic;

namespace ProgParty.SiliconAngle.ApiUniversal.Execute
{
    public class OverviewExecute
    {
        public Parameter.OverviewParameter Parameters = new Parameter.OverviewParameter();

        public List<OverviewResult> Result;

        public bool Execute()
        {
            try
            {
                Result = new OverviewScrape(Parameters).Execute();
                return true;
            }
            catch(System.Exception e)
            {
                return false;
            }
        }
    }
}
