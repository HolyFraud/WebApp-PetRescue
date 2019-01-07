using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public partial class JSONTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                string rowJSON = webClient.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=3186,+VIC&key=AIzaSyAEwsGJsYxlLkADDUif5oZ1oy7UG9VXOic");
                RootObject item = JsonConvert.DeserializeObject<RootObject>(rowJSON);

                string postcode = item.results[0].address_components[0].long_name;
                string state = item.results[0].address_components[3].short_name;
                double lat = item.results[0].geometry.location.lat;
                double lon = item.results[0].geometry.location.lng;

                var s = 1;
            }

        }

        
    }
}