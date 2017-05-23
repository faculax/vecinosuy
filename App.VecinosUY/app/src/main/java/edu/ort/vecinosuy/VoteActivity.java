package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Patterns;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.StringTokenizer;
import java.util.regex.Pattern;

import android.app.AlertDialog.Builder;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONObject;

import logic.AnnouncementContract;
import logic.AnnouncementDbHelper;
import logic.Repository;

import static edu.ort.vecinosuy.R.string.vote;

public class VoteActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_vote);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);

        FloatingActionButton v=(FloatingActionButton)this.findViewById(R.id.addVoteBtn);
        v.setOnClickListener(this);

        Bundle voteBoundle = getIntent().getExtras();
        ArrayList<String> votes = new ArrayList<String>();
        if(voteBoundle != null) {
            votes = voteBoundle.getStringArrayList("votes");
        }
        final ListView listview = (ListView) findViewById(R.id.voteListView);

        final ArrayList<String> list = new ArrayList<String>();
        for (int i = 0; i < votes.size(); ++i) {
            list.add(votes.get(i));
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
                final int voteId = Integer.parseInt(st.nextToken());


                DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        switch (which){
                            case DialogInterface.BUTTON_POSITIVE:
                                putVote(voteId+"", Repository.getInstance().votesRepository.get(voteId).EndDate,
                                        Repository.getInstance().votesRepository.get(voteId).YesNoQuestion,
                                        Repository.getInstance().votesRepository.get(voteId).Yes+1,
                                        Repository.getInstance().votesRepository.get(voteId).No);
                                break;

                            case DialogInterface.BUTTON_NEGATIVE:
                                putVote(voteId+"", Repository.getInstance().votesRepository.get(voteId).EndDate,
                                        Repository.getInstance().votesRepository.get(voteId).YesNoQuestion,
                                        Repository.getInstance().votesRepository.get(voteId).Yes,
                                        Repository.getInstance().votesRepository.get(voteId).No+1);
                                break;
                        }
                    }
                };
                showVote(dialogClickListener, Repository.getInstance().votesRepository.get(voteId).YesNoQuestion);
            }
        });


        }

    public void showVote (DialogInterface.OnClickListener dialogClickListener, String question) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setMessage("vote: " + question).setPositiveButton("Yes", dialogClickListener)
                .setNegativeButton("No", dialogClickListener).show();
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.addVoteBtn:
            /*    Intent i = new Intent(getApplicationContext(), AnnouncementFormActivity.class);
                Bundle announcementsBoundle = new Bundle();
                announcementsBoundle.putBoolean("new", true);
                i.putExtras(announcementsBoundle);
                startActivity(i);
                break;*/
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

    private void putVote(String voteId, String endDate, String yesNoQuestion, int yes, int no){
        String serverAddr = getResources().getString(R.string.serverAddr) + "votes/" + voteId;
        RequestQueue queue = Volley.newRequestQueue(this);
        HashMap<String, String> params = new HashMap<String, String>();
        params.put("EndDate", endDate);
        params.put("Deleted", "false");
        params.put("YesNoQuestion", yesNoQuestion);
        params.put("Yes", yes+"");
        params.put("No", no+"");
        JsonObjectRequest request_json = new JsonObjectRequest(Request.Method.PUT, serverAddr, new JSONObject(params),
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        finish();
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                VolleyLog.e("Error: ", error.getMessage());
            }
        }

        ){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);

                return params;
            }
        };
        queue.add(request_json);
        //  finish();
    }
    private String getLogedUserEmail(){
        Account[] accounts = AccountManager.get(this).getAccounts();
        Pattern emailPattern = Patterns.EMAIL_ADDRESS;
        String possibleEmail = "";

        for (Account account : accounts) {
            if (emailPattern.matcher(account.name).matches()) {
                possibleEmail = account.name;
            }
        }
        return possibleEmail;
    }
}
