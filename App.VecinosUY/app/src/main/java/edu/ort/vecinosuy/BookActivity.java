package edu.ort.vecinosuy;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class BookActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);

        FloatingActionButton v=(FloatingActionButton)this.findViewById(R.id.addBookgBtn);
        v.setOnClickListener(this);

        Bundle bookBoundle = getIntent().getExtras();
        ArrayList<String> books = new ArrayList<String>();
        if(bookBoundle != null) {
            books = bookBoundle.getStringArrayList("books");
        }
        final ListView listview = (ListView) findViewById(R.id.bookListView);

        final ArrayList<String> list = new ArrayList<String>();
        for (int i = 0; i < books.size(); ++i) {
            list.add(books.get(i));
        }
        final StableArrayAdapter adapter = new StableArrayAdapter(this,
                android.R.layout.simple_list_item_1, list);
        listview.setAdapter(adapter);

        listview.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, final View view,
                                    int position, long id) {
               /* final String item = (String) parent.getItemAtPosition(position);
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
                i.putExtras(announcementsBoundle);
                startActivity(i); */
            }
        });


        }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.addBookgBtn:
                try {
                    Intent i = new Intent(getApplicationContext(), NewBookActivity.class);
                    startActivity(i);
                } catch (Exception e) {
                    e.printStackTrace();
                }
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
}
