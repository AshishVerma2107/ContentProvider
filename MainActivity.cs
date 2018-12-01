using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Content;
using Android.Database;
using System.Collections.Generic;
using Java.Util;

namespace ContentProviderDemo
{
    [Activity(Label = "ContentProviderDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private SimpleCursorAdapter adapter;
        ListView contactlist;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            Button readcontact = (Button)FindViewById(Resource.Id.btnreadcontacts);
            Button addnewcontact = (Button)FindViewById(Resource.Id.btnaddnewcontacts);

            readcontact.Click += Readcontact_Click;
            addnewcontact.Click += Addnewcontact_Click;
            
            
        }

        private void Addnewcontact_Click(object sender, System.EventArgs e)
        {

            Intent addcontact = new Intent(this, typeof(Addnewcontact));
            StartActivity(addcontact);
        }

        private void Readcontact_Click(object sender, System.EventArgs e)
        {
            var uri = ContactsContract.Contacts.ContentUri;
            contactlist = (ListView)FindViewById(Resource.Id.contactlist);
            string[] projection = {
                                     ContactsContract.Contacts.InterfaceConsts.Id,
                                     ContactsContract.Contacts.InterfaceConsts.DisplayName
                                   };
            var loader = new CursorLoader(this, uri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            var fromColumns = new string[] { ContactsContract.Contacts.InterfaceConsts.DisplayName };
            var toControlIds = new int[] { Android.Resource.Id.Text1 };
            adapter = new SimpleCursorAdapter(this, Android.Resource.Layout.SimpleListItem1, cursor, fromColumns, toControlIds);
            contactlist.Adapter = adapter;
        }
    }
}
