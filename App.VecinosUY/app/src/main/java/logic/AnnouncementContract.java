package logic;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.provider.BaseColumns;

/**
 * Created by Facu on 5/12/17.
 */

public final class AnnouncementContract {
    // To prevent someone from accidentally instantiating the contract class,
    // make the constructor private.
    private AnnouncementContract() {}

    /* Inner class that defines the table contents */
    public static class AnnouncementEntry {
        public static final String TABLE_NAME = "announcement";
        public static final String COLUMN_NAME_ANNOUNCEMENT_ID = "announcementId";
        public static final String COLUMN_NAME_TITLE = "title";
        public static final String COLUMN_NAME_BODY = "body";
        public static final String COLUMN_NAME_IMAGE = "image";
    }

    public static final String SQL_CREATE_ANNOUNCEMENT_ENTRIES =
            "CREATE TABLE " + AnnouncementEntry.TABLE_NAME + " (" +
                    AnnouncementEntry.COLUMN_NAME_ANNOUNCEMENT_ID + " INTEGER PRIMARY KEY," +
                    AnnouncementEntry.COLUMN_NAME_TITLE + " TEXT," +
                    AnnouncementEntry.COLUMN_NAME_BODY + " TEXT," +
                    AnnouncementEntry.COLUMN_NAME_IMAGE + " TEXT)";

    public static final String SQL_DELETE_ANNOUNCEMENT_ENTRIES =
            "DROP TABLE IF EXISTS " + AnnouncementEntry.TABLE_NAME;

}

