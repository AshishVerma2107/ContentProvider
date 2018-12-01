using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;

namespace ContentProviderDemo
{
    [Activity(Label = "Addnewcontact")]
    public class Addnewcontact : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addnewcontact);

            Button addnewcontact = (Button)FindViewById(Resource.Id.btnadd);
            Button cancel = (Button)FindViewById(Resource.Id.btncancel);

            addnewcontact.Click += Addnewcontact_Click;
            cancel.Click += Cancel_Click;
            // Create your application here
            
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            OnBackPressed();
        }

        private void Addnewcontact_Click(object sender, EventArgs e)
        {
            EditText txtfirstname = (EditText)FindViewById(Resource.Id.edtfirstname);
            EditText txtlastname = (EditText)FindViewById(Resource.Id.edtlastname);
            EditText txtphoneno = (EditText)FindViewById(Resource.Id.edtphoneno);
            string lastname = (txtlastname.Text).ToString();
            string firstname = (txtfirstname.Text).ToString();
            string phone = (txtphoneno.Text).ToString();

            List<ContentProviderOperation> contactadd = new List<ContentProviderOperation>();

            ContentProviderOperation.Builder builder =
                ContentProviderOperation.NewInsert(ContactsContract.RawContacts.ContentUri);
            builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountType, null);
            builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountName, null);
            contactadd.Add(builder.Build());

            builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
            builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
            builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                              ContactsContract.CommonDataKinds.StructuredName.ContentItemType);
            builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.FamilyName, lastname);
            builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.GivenName, firstname);
            contactadd.Add(builder.Build());

            //Number
               builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
               builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
               builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                                 ContactsContract.CommonDataKinds.Phone.ContentItemType);
               builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, phone);
               builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Type,
                                 ContactsContract.CommonDataKinds.Phone.InterfaceConsts.TypeCustom);
               builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Mobile");
               contactadd.Add(builder.Build());

            //Add the new contact
            ContentProviderResult[] res;
            try
            {
                res = ContentResolver.ApplyBatch(ContactsContract.Authority, contactadd);
                Toast.MakeText(this, "Contact added sucessfully", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(this, "Contact not added", ToastLength.Long).Show();
            }
        }
    }
}