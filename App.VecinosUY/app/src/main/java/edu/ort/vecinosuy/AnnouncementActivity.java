package edu.ort.vecinosuy;

import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.StringTokenizer;

import logic.AnnouncementContract;
import logic.AnnouncementDbHelper;
import logic.Repository;

import static java.security.AccessController.getContext;

public class AnnouncementActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_announcement);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);

        FloatingActionButton v=(FloatingActionButton)this.findViewById(R.id.fab);
        v.setOnClickListener(this);

        Bundle announcementsBoundle = getIntent().getExtras();
        ArrayList<String> announcements = new ArrayList<String>();
        String token = getString(R.string.vecinosUySessionToken);
        if(announcementsBoundle != null) {
            announcements = announcementsBoundle.getStringArrayList("announcements");
            if(announcementsBoundle.getBoolean("serverdown")){
                FloatingActionButton v2=(FloatingActionButton)this.findViewById(R.id.fab);
                v2.setVisibility(View.GONE);
            }
        }
        final ListView listview = (ListView) findViewById(R.id.listview);

        final ArrayList<String> list = new ArrayList<String>();
        for (int i = 0; i < announcements.size(); ++i) {
            list.add(announcements.get(i));
        }
        final StableArrayAdapter adapter = new StableArrayAdapter(this,
                android.R.layout.simple_list_item_1, list);
        listview.setAdapter(adapter);

        listview.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, final View view,
                                    int position, long id) {
                final String item = (String) parent.getItemAtPosition(position);
                StringTokenizer st = new StringTokenizer(item, ":");
                int annauncementId = Integer.parseInt(st.nextToken());
                String announcementTitle = st.nextToken();
                Intent i = new Intent(getApplicationContext(), AnnouncementFormActivity.class);
                Bundle announcementsBoundle = new Bundle();
                announcementsBoundle.putBoolean("new", false);
                announcementsBoundle.putInt("announcementId", annauncementId);
                announcementsBoundle.putString("announcementTitle", announcementTitle);
                announcementsBoundle.putString("announcementImage", Repository.getInstance().announcementImage.get(annauncementId));
                announcementsBoundle.putString("announcementBody", Repository.getInstance().announcementBody.get(annauncementId));
                // chequear si el anuncio es favorito
                checkFavorite(annauncementId,announcementsBoundle);
                //
                i.putExtras(announcementsBoundle);
                startActivity(i);
            }
        });


        }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.fab:
                Intent i = new Intent(getApplicationContext(), AnnouncementFormActivity.class);
                Bundle announcementsBoundle = new Bundle();
                announcementsBoundle.putBoolean("new", true);
                i.putExtras(announcementsBoundle);
                startActivity(i);
                break;
        }
    }

    private class StableArrayAdapter extends ArrayAdapter<String> {

        HashMap<String, Integer> mIdMap = new HashMap<String, Integer>();

        public StableArrayAdapter(Context context, int textViewResourceId,
                                  List<String> objects) {
            super(context, textViewResourceId, objects);
            for (int i = 0; i < objects.size(); ++i) {
                mIdMap.put(objects.get(i), i);
            }
        }

        @Override
        public long getItemId(int position) {
            String item = getItem(position);
            return mIdMap.get(item);
        }

        @Override
        public boolean hasStableIds() {
            return true;
        }

    }

    @Override
    public boolean onSupportNavigateUp() {
        onBackPressed();
        return true;
    }
    private void checkFavorite(int annauncementId, Bundle announcementsBoundle){
        AnnouncementDbHelper mDbHelper = new AnnouncementDbHelper(this);
        SQLiteDatabase db = mDbHelper.getReadableDatabase();
        // Define a projection that specifies which columns from the database
        // you will actually use after this query.
        String[] projection = {
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID,
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_TITLE
        };
        // Filter results WHERE "title" = 'My Title'
        String selection = AnnouncementContract.AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID + " = ?";
        String[] selectionArgs = { annauncementId+"" };
        // How you want the results sorted in the resulting Cursor
        String sortOrder =
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID + " DESC";
        Cursor cursor = db.query(
                AnnouncementContract.AnnouncementEntry.TABLE_NAME,                     // The table to query
                projection,                               // The columns to return
                selection,                                // The columns for the WHERE clause
                selectionArgs,                            // The values for the WHERE clause
                null,                                     // don't group the rows
                null,                                     // don't filter by row groups
                sortOrder                                 // The sort order
        );
        if(cursor.getCount()>0){
            announcementsBoundle.putBoolean("announcementFav", true);
        }else {
            announcementsBoundle.putBoolean("announcementFav", false);
        }
    }
}
