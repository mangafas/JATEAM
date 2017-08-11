//where are the comments at?

using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views.InputMethods;

namespace App5
{
    [Activity(Label = "My Shopping List", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.AdjustPan |SoftInput.StateHidden)]
    public class MainActivity : ListActivity
    {

        

        public List<string> Items { get; set; }

        ArrayAdapter<string> adapter;

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("SHOPPING_LIST", FileCreationMode.Private);


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            
            Items = new List<string>();

            LoadList();

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, Items);
            ListAdapter = adapter;

            Button addButton = FindViewById<Button>(Resource.Id.AddButton);
            addButton.Click += delegate
            {
                EditText itemText = FindViewById<EditText>(Resource.Id.itemText);
                string item = itemText.Text;

                if(item == "" || item == null)
                {
                    //just return
                    return;
                }

                Items.Add(item);
                adapter.Add(item);

                adapter.NotifyDataSetChanged();

                itemText.Text = "";


                UpdateStoredData();
            };

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            RunOnUiThread(() =>
            {
                AlertDialog.Builder builder;
                builder = new AlertDialog.Builder(this);
                builder.SetTitle("Confirm");
                builder.SetMessage("Do you want to remove this item?");
                builder.SetCancelable(true);

                builder.SetPositiveButton("OK", delegate
                {
                    var item = Items[position];
                    Items.Remove(item);
                    adapter.Remove(item);

                    l.ClearChoices();
                    l.RequestLayout();

                    UpdateStoredData();
                });

                builder.SetNegativeButton("Cancel", delegate
                {
                    //user cancelled
                    return;
                });

                builder.Show();

            });

        }



        public void LoadList()
        {
            int count = prefs.GetInt("itemCount", 0);

            if(count > 0)
            {
                Toast.MakeText(this, "Getting shopping list ready...", Android.Widget.ToastLength.Short).Show();

                for (int i = 0; i <= count; i++)
                {
                    string item = prefs.GetString(i.ToString(), null);
                    if (item != null)
                    {
                        Items.Add(item);
                    }
                }
            }

        }//end of LoadList

        public void UpdateStoredData()
        {
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Clear();
            editor.Commit();


            editor = prefs.Edit();

            editor.PutInt("itemCount", Items.Count);

            int counter = 0;

            foreach(string item in Items)
            {
                editor.PutString(counter.ToString(), item);
                counter++;
            }

            editor.Apply();


        }//end of UpadateStoreData

    }//end of Class

}
