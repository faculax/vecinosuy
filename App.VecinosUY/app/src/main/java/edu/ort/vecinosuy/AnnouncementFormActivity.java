package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.Image;
import android.provider.MediaStore;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.util.Patterns;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.android.volley.AuthFailureError;
import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.JsonRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.io.ByteArrayOutputStream;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Pattern;

import static android.provider.ContactsContract.CommonDataKinds.Website.URL;

public class AnnouncementFormActivity extends AppCompatActivity implements View.OnClickListener {

    Bitmap image;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_announcement_form);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);
        Button v=(Button)this.findViewById(R.id.submitBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.photoBtn);
        v.setOnClickListener(this);
        Bundle announcementsBoundle = getIntent().getExtras();
        boolean isNew = true;
        if(announcementsBoundle != null) {
            isNew = announcementsBoundle.getBoolean("new");
            if (!isNew){
                EditText titleField   = (EditText)findViewById(R.id.titleTxt);
                titleField.setFocusable(false);
                titleField.setText(announcementsBoundle.getString("announcementTitle"));
                EditText bodyField   = (EditText)findViewById(R.id.bodyTxt);
                bodyField.setFocusable(false);
                bodyField.setText(announcementsBoundle.getString("announcementBody"));
                Button photoButton   = (Button)findViewById(R.id.photoBtn);
                photoButton.setVisibility(View.GONE);
                Button sendButton   = (Button)findViewById(R.id.submitBtn);
                sendButton.setVisibility(View.GONE);
                TextView lbl   = (TextView)findViewById(R.id.labelAnnForm);
                lbl.setText("Anuncio:");
                ImageView iv = (ImageView) findViewById(R.id.annImgView);
                iv.setImageBitmap(this.StringToBitMap(announcementsBoundle.getString("announcementImage")));
            }
        }
    }
    @Override
    public boolean onSupportNavigateUp() {
        onBackPressed();
        return true;
    }
    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.submitBtn:
                postAnnouncement();
                break;
            case R.id.photoBtn:
                Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                if (takePictureIntent.resolveActivity(getPackageManager()) != null) {
                    startActivityForResult(takePictureIntent, 1);
                }
                break;
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == 1 && resultCode == RESULT_OK) {
            Bundle extras = data.getExtras();
            Bitmap imageBitmap = (Bitmap) extras.get("data");
            ImageView mImageView = (ImageView) findViewById(R.id.annImgView);
            mImageView.setImageBitmap(imageBitmap);
            image = imageBitmap;
        }
    }

    private void postAnnouncement(){
        String serverAddr = getResources().getString(R.string.serverAddr) + "announcements/";
        RequestQueue queue = Volley.newRequestQueue(this);
        HashMap<String, String> params = new HashMap<String, String>();
        EditText titleField   = (EditText)findViewById(R.id.titleTxt);
        String title = titleField.getText().toString();
        EditText bodyField   = (EditText)findViewById(R.id.bodyTxt);
        String body = bodyField.getText().toString();
        params.put("Title", title);
        params.put("Deleted", "false");
        params.put("Body", body);
        params.put("Image", this.BitMapToString(image));
        JsonObjectRequest request_json = new JsonObjectRequest(serverAddr, new JSONObject(params),
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
    public static String BitMapToString(Bitmap bitmap){
        ByteArrayOutputStream baos=new  ByteArrayOutputStream();
        bitmap.compress(Bitmap.CompressFormat.PNG,100, baos);
        byte [] b=baos.toByteArray();
        String temp= Base64.encodeToString(b, Base64.DEFAULT);
        return temp;
    }

    public static Bitmap StringToBitMap(String encodedString){
        try {
            byte [] encodeByte=Base64.decode(encodedString,Base64.DEFAULT);
            Bitmap bitmap= BitmapFactory.decodeByteArray(encodeByte, 0, encodeByte.length);
            return bitmap;
        } catch(Exception e) {
            e.getMessage();
            return null;
        }
    }
}
