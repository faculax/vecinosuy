package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Patterns;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Pattern;

import logic.AnnouncementContract;
import logic.AnnouncementDbHelper;
import logic.Repository;
import logic.voteDto;

import static android.R.attr.id;
import static android.R.string.no;
import static android.R.string.yes;
import static edu.ort.vecinosuy.R.string.serverAddr;

public class MainActivity extends AppCompatActivity implements OnClickListener{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Bundle login_main_boundle = getIntent().getExtras();
        TextView helloMesagge = (TextView) findViewById(R.id.helloUser);
        String userName = "";
        String token = getString(R.string.vecinosUySessionToken);
        if(login_main_boundle != null) {
            userName = login_main_boundle.getString("userName");
            token = login_main_boundle.getString("token");
            helloMesagge.setText("Hola " + userName );
        }
        Button v=(Button)this.findViewById(R.id.announcementsBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.accountStateBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.mtngsBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.voteBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.reserveBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.agentaBtn);
        v.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.announcementsBtn:
                manageAnnouncements();
              //  finish();
                break;
            case R.id.accountStateBtn:
                manageAccounts();
                break;
            case R.id.mtngsBtn:
                manageMeetings();
                break;
            case R.id.voteBtn:
                manageVotes();
                break;
            case R.id.reserveBtn:
                loadServices();
                break;
            case R.id.agentaBtn:
                manageContacts();
                break;
        }

    }
    private void loadServices() {
        Repository.getInstance().servicesRepository.clear();
        String serverAddr = getResources().getString(R.string.serverAddr) + "services";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                        JSONArray jsonArray;
                        try {
                            jsonArray = new JSONArray(response);
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String id = jsonObject.getString ("ServiceId");
                                Repository.getInstance().servicesRepository.add(id);

                            }
                        } catch (JSONException e) {}
                        catch (Exception e) {}
                        manageBooks();
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                showToast();

            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
    }
    private void manageBooks() {
        String serverAddr = getResources().getString(R.string.serverAddr) + "bookings";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                        JSONArray jsonArray;
                        ArrayList<String> books = new ArrayList<String>();
                        try {
                            jsonArray = new JSONArray(response);
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String id = jsonObject.getString ("BookingId");
                                String user = jsonObject.getString ("User");
                                String service = jsonObject.getString ("Service");
                                String bookedFrom = jsonObject.getString ("BookedFrom");
                                String bookedTo = jsonObject.getString ("BookedTo");
                                String deleted = jsonObject.getString ("Deleted");
                                books.add(id +": " + user + " " +
                                        service + " " +
                                        bookedFrom + " " +
                                        bookedTo);

                            }
                        } catch (JSONException e) {
                        }
                        catch (Exception e) {
                        }
                        Intent i = new Intent(getApplicationContext(), BookActivity.class);
                        Bundle bundleBooks = new Bundle();
                        bundleBooks.putStringArrayList("books", books);
                        i.putExtras(bundleBooks);
                        startActivity(i);

                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
    }
    private void manageVotes() {
        String serverAddr = getResources().getString(R.string.serverAddr) + "votes";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                       JSONArray jsonArray;
                        ArrayList<String> votes = new ArrayList<String>();
                        Repository.getInstance().votesRepository.clear();
                        try {
                            jsonArray = new JSONArray(response);
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String question = jsonObject.getString("YesNoQuestion");
                                String yes = jsonObject.getString("Yes");
                                String id = jsonObject.getString("VoteId");
                                String no = jsonObject.getString("No");
                                String endDate = jsonObject.getString("EndDate");
                                votes.add(id +": " + question +  " SI: "+ yes + " NO: " + no + "\n valido hasta: " + endDate);
                                voteDto vdto = new voteDto();
                                vdto.Deleted = false;
                                vdto.EndDate = endDate;
                                vdto.No = Integer.parseInt(no);
                                vdto.VoteId = Integer.parseInt(id);
                                vdto.Yes = Integer.parseInt(yes);
                                vdto.YesNoQuestion = question;
                                Repository.getInstance().votesRepository.put(vdto.VoteId, vdto);

                            }
                        } catch (JSONException e) {
                        }
                        catch (Exception e) {
                        }
                        Intent i = new Intent(getApplicationContext(), VoteActivity.class);
                        Bundle bundleVotes = new Bundle();
                        bundleVotes.putStringArrayList("votes", votes);
                        i.putExtras(bundleVotes);
                        startActivity(i);

                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                showToast();
            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
    }

    private void manageMeetings() {
        String serverAddr = getResources().getString(R.string.serverAddr) + "meetings";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                        JSONArray jsonArray;
                        ArrayList<String> meetings = new ArrayList<String>();
                        try {
                            jsonArray = new JSONArray(response);
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String date = jsonObject.getString("Date");
                                String subject = jsonObject.getString("Subject");
                                meetings.add(subject + " " + date);
                            }
                        } catch (JSONException e) {
                        }
                        catch (Exception e) {
                        }
                        Intent i = new Intent(getApplicationContext(), MeetingActivity.class);
                        Bundle meetingsBundle = new Bundle();
                        meetingsBundle.putStringArrayList("meetings", meetings);
                        i.putExtras(meetingsBundle);
                        startActivity(i);

                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                showToast();
            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
    }

    private void manageAccounts() {
        String serverAddr = getResources().getString(R.string.serverAddr) + "accountstates/byId";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                        JSONArray jsonArray;
                        ArrayList<String> accountStates = new ArrayList<String>();
                        try {
                            jsonArray = new JSONArray(response);
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String month = jsonObject.getString("Month");
                                String year = jsonObject.getString("Year");
                                String ammount = jsonObject.getString("Ammount");
                                accountStates.add(month + "/" + year + " total: " + ammount);
                            }
                        } catch (JSONException e) {
                        }
                        catch (Exception e) {
                        }
                        Intent i = new Intent(getApplicationContext(), AccountStateActivity.class);
                        Bundle accountStateBundle = new Bundle();
                        accountStateBundle.putStringArrayList("accountStates", accountStates);
                        i.putExtras(accountStateBundle);
                            startActivity(i);

                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                showToast();
            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
    }

    private void manageContacts() {
        String serverAddr = getResources().getString(R.string.serverAddr) + "contacts";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                        JSONArray jsonArray;
                        ArrayList<String> contacts = new ArrayList<String>();
                        try {
                            jsonArray = new JSONArray(response);
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String name = jsonObject.getString("Name");
                                String phone = jsonObject.getString("Phone");
                                String apartment = jsonObject.getString("Apartment");
                                contacts.add(name + " - " + phone + " - Apt: " + apartment);
                            }
                        } catch (JSONException e) {
                        }
                        catch (Exception e) {
                        }
                        Intent i = new Intent(getApplicationContext(), ContactActivity.class);
                        Bundle contactBundle = new Bundle();
                        contactBundle.putStringArrayList("contacts", contacts);
                        i.putExtras(contactBundle);
                        startActivity(i);

                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                showToast();
            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
    }

    private void manageAnnouncements() {
        String serverAddr = getResources().getString(R.string.serverAddr) + "announcements/";
        RequestQueue queue = Volley.newRequestQueue(this);
        StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                new Response.Listener<String>() {

                    @Override
                    public void onResponse(String response) {
                        JSONArray jsonArray;
                        ArrayList<String> announcements = new ArrayList<String>();
                        try {
                            jsonArray = new JSONArray(response);
                            Repository.getInstance().announcementBody.clear();
                            Repository.getInstance().announcementImage.clear();
                            for (int i = 0; i< jsonArray.length(); i++) {
                                JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                String title = jsonObject.getString("Title");
                                String id = jsonObject.getString("AnnouncementId");
                                String body = jsonObject.getString("Body");
                                String image = jsonObject.getString("Image");
                                announcements.add(id + ": " + title);
                                Repository.getInstance().announcementBody.put(Integer.parseInt(id),body);
                                Repository.getInstance().announcementImage.put(Integer.parseInt(id),image);
                            }
                        } catch (JSONException e) {
                        }
                        catch (Exception e) {
                            String ee = e.toString();
                        }
                        Intent i = new Intent(getApplicationContext(), AnnouncementActivity.class);
                        Bundle announcementsBoundle = new Bundle();
                        announcementsBoundle.putStringArrayList("announcements", announcements);
                        announcementsBoundle.putBoolean("serverdown", false);
                        i.putExtras(announcementsBoundle);
                        startActivity(i);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                //mTxtDisplay.setText("Error de logueo, reintente");
                // cargar anuncios desde la base
                readFromLocalDB();

            }
        })
        {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);
                params.put("Content-Type", "application/json");

                return params;
            }
        };
        queue.add(stringRequest);
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
    private void readFromLocalDB(){
        AnnouncementDbHelper mDbHelper = new AnnouncementDbHelper(this);
        SQLiteDatabase db = mDbHelper.getReadableDatabase();
        // Define a projection that specifies which columns from the database
        // you will actually use after this query.
        String[] projection = {
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID,
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_TITLE,
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_BODY,
                AnnouncementContract.AnnouncementEntry.COLUMN_NAME_IMAGE
        };
        // Filter results WHERE "title" = 'My Title'
        String selection = AnnouncementContract.AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID + " > ?";
        String[] selectionArgs = { "0" };
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
        int a = cursor.getCount();
        ArrayList<String> announcements = new ArrayList<String>();
            Repository.getInstance().announcementBody.clear();
        cursor.moveToFirst();
            Repository.getInstance().announcementImage.clear();
            for (int i = 0; i< cursor.getCount(); i++) {
                String title = cursor.getString(cursor.getColumnIndex(AnnouncementContract.AnnouncementEntry.COLUMN_NAME_TITLE));
                String id = cursor.getString(cursor.getColumnIndex(AnnouncementContract.AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID));
                String body = cursor.getString(cursor.getColumnIndex(AnnouncementContract.AnnouncementEntry.COLUMN_NAME_BODY));
                String image = cursor.getString(cursor.getColumnIndex(AnnouncementContract.AnnouncementEntry.COLUMN_NAME_IMAGE));
                announcements.add(id + ": " + title);
                Repository.getInstance().announcementBody.put(Integer.parseInt(id),body);
                Repository.getInstance().announcementImage.put(Integer.parseInt(id),image);
                cursor.moveToNext();
            }
        cursor.close();
        Intent i = new Intent(getApplicationContext(), AnnouncementActivity.class);
        Bundle announcementsBoundle = new Bundle();
        announcementsBoundle.putStringArrayList("announcements", announcements);
        announcementsBoundle.putBoolean("serverdown", true);
        i.putExtras(announcementsBoundle);
        startActivity(i);
    }
public void showToast(){
    Toast.makeText(this, R.string.connError, Toast.LENGTH_SHORT).show();
}
}
