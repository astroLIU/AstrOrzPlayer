using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Dapper;
namespace AstrOrzPlayer
{
    public class DBHelper
    {
        public DBHelper()
        {
        }
        public static string connStr = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "Library.db";
        private static SQLiteConnection getConnection()
        {
            System.Data.SQLite.SQLiteConnection conn = null;
            conn = new System.Data.SQLite.SQLiteConnection(connStr);
            return conn;
        }
        public static SQLiteTransaction BeginTransaction(SQLiteConnection conn)
        {
            SQLiteTransaction tran = conn.BeginTransaction();
            return tran;
        }
        public static int DBFuncSQLite(string sql, object model, SQLiteTransaction tran, SQLiteConnection conn)
        {
            try
            {
                int i = 0;
                i = conn.Execute(sql, model, tran);
                tran.Commit();
                conn.Dispose();
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int DBFuncSQLite(string sql, object model)
        {
            try
            {
                int i = 0;
                System.Data.SQLite.SQLiteConnection conn = getConnection();
                i = conn.Execute(sql, model);
                conn.Dispose();
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int DBFuncSQLite(string sql)
        {
            try
            {
                int i = 0;
                System.Data.SQLite.SQLiteConnection conn = getConnection();
                i = conn.Execute(sql);
                conn.Dispose();
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static List<T> DBQuerySQLite<T>(string sql)
        {
            try
            {
                System.Data.SQLite.SQLiteConnection conn = getConnection();
                List<T> list = conn.Query<T>(sql).AsList<T>();
                conn.Dispose();
                return list;
            }
            catch
            {
                return default(List<T>);
            }
        }
        public static T GetInfoByID<T>(string tableName, int id)
        {
            try
            {
                System.Data.SQLite.SQLiteConnection conn = getConnection();
                string sql = "select * from " + tableName + " where id=@id";
                T t = conn.QueryFirstOrDefault<T>(sql, new { id });
                conn.Dispose();
                return t;
            }
            catch
            {
                return default(T);
            }
        }
        public static T GetInfoByCol<T>(string tableName, string colName, string colValue)
        {
            try
            {
                System.Data.IDbConnection conn = getConnection();
                string sql = "select * from " + tableName + " where " + colName + "='" + colValue + "'";
                T t = (T)conn.QueryFirstOrDefault<T>(sql);
                conn.Dispose();
                return t;
            }
            catch
            {
                return default(T);
            }
        }
        public static int SetSeq0ByTableName(string tbName)
        {
            string sql = "update sqlite_sequence set seq = 0 where name = '" + tbName + "'";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int DeleteFromTableName(string tbName)
        {
            string sql = "DELETE FROM " + tbName + "";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static void DeleteAndSetSeq0ToAllTable()
        {
            DBHelper.DeleteFromTableName("tbl_Folders");
            DBHelper.DeleteFromTableName("tbl_PlayList");
            DBHelper.DeleteFromTableName("tbl_PlayLists");
            DBHelper.DeleteFromTableName("tbl_Library");
            DBHelper.DeleteFromTableName("tbl_LibraryFolders");
            DBHelper.DeleteFromTableName("tbl_Favorite");
            DBHelper.SetSeq0ByTableName("tbl_Folders");
            DBHelper.SetSeq0ByTableName("tbl_PlayList");
            DBHelper.SetSeq0ByTableName("tbl_PlayLists");
            DBHelper.SetSeq0ByTableName("tbl_Library");
            DBHelper.SetSeq0ByTableName("tbl_LibraryFolders");
            DBHelper.SetSeq0ByTableName("tbl_Favorite");
        }
        public static PlayList GetPlayListByPath(string path)
        {
            PlayList p = GetInfoByCol<PlayList>("tbl_PlayList", "FilePath", path);
            return p;
        }
        public static PlayList GetLibraryByPath(string path)
        {
            PlayList p = GetInfoByCol<PlayList>("tbl_Library", "FilePath", path);
            return p;
        }
        public static int InsertPlayList(PlayList item)
        {
            string sql = "INSERT INTO tbl_PlayList (Artist,Title,Album,Duration,DurationString,FileType,FilePath,MusicType,PlayCount,Rate,FavSong,StartTime,EndTime,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@Artist,@Title,@Album,@Duration,@DurationString,@FileType,@FilePath,@MusicType,@PlayCount,@Rate,@FavSong,@StartTime,@EndTime,@CreateDate,@UpdateDate)";
            item.ID = GetCount("tbl_PlayList") + 1;
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int InsertPlayListFromList(List<PlayList> item)
        {
            string sql = "INSERT INTO tbl_PlayList (Artist,Title,Album,Duration,DurationString,FileType,FilePath,MusicType,PlayCount,Rate,FavSong,StartTime,EndTime,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@Artist,@Title,@Album,@Duration,@DurationString,@FileType,@FilePath,@MusicType,@PlayCount,@Rate,@FavSong,@StartTime,@EndTime,@CreateDate,@UpdateDate)";
            SQLiteConnection conn = getConnection();
            conn.Open();
            SQLiteTransaction trans = BeginTransaction(conn);
            int i = DBFuncSQLite(sql, item, trans, conn);
            return i;
        }
        public static int UpdatePlayList(PlayList item)
        {
            string sql = "UPDATE tbl_PlayList SET Artist=@Artist,Title=@Title,Album=@Album,Duration=@Duration,DurationString=@DurationString,FileType=@FileType,FilePath=@FilePath," +
                "MusicType=@MusicType,PlayCount=@PlayCount,Rate=@Rate,FavSong=@FavSong,StartTime=@StartTime,EndTime=@EndTime,CreateDate=@CreateDate,UpdateDate=@UpdateDate WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static List<PlayList> GetPlayListAll()
        {
            string sql = "select * from tbl_PlayList";
            List<PlayList> list = DBQuerySQLite<PlayList>(sql);
            return list;
        }
        public static int DeletePlayList(PlayList item)
        {
            string sql = "DELETE FROM tbl_PlayList WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int DeletePlayListAll()
        {
            string sql = "DELETE FROM tbl_PlayList";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int InsertPlayLists(PlayLists item)
        {
            string sql = "INSERT INTO tbl_PlayLists (PlayListName,Artist,Title,Album,Duration,DurationString,FileType,FilePath,MusicType,PlayCount,Rate,FavSong,StartTime,EndTime,FromFilePath,FileLastModifyDate,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@PlayListName,@Artist,@Title,@Album,@Duration,@DurationString,@FileType,@FilePath,@MusicType,@PlayCount,@Rate,@FavSong,@StartTime,@EndTime,@FromFilePath,@FileLastModifyDate,@CreateDate,@UpdateDate)";
            item.ID = GetCount("tbl_PlayLists") + 1;
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int InsertPlayListsFromList(List<PlayLists> item)
        {
            string sql = "INSERT INTO tbl_PlayLists (PlayListName,Artist,Title,Album,Duration,DurationString,FileType,FilePath,MusicType,PlayCount,Rate,FavSong,StartTime,EndTime,FromFilePath,FileLastModifyDate,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@PlayListName,@Artist,@Title,@Album,@Duration,@DurationString,@FileType,@FilePath,@MusicType,@PlayCount,@Rate,@FavSong,@StartTime,@EndTime,@FromFilePath,@FileLastModifyDate,@CreateDate,@UpdateDate)";
            SQLiteConnection conn = getConnection();
            conn.Open();
            SQLiteTransaction trans = BeginTransaction(conn);
            int i = DBFuncSQLite(sql, item, trans, conn);
            return i;
        }
        public static int UpdatePlayLists(PlayLists item)
        {
            string sql = "UPDATE tbl_PlayLists SET PlayListName=@PlayListName,Artist=@Artist,Title=@Title,Album=@Album,Duration=@Duration,DurationString=@DurationString,FileType=@FileType,FilePath=@FilePath," +
                "MusicType=@MusicType,PlayCount=@PlayCount,Rate=@Rate,FavSong=@FavSong,StartTime=@StartTime,EndTime=@EndTime,FromFilePath=@FromFilePath,FileLastModifyDate=@FileLastModifyDate,CreateDate=@CreateDate,UpdateDate=@UpdateDate WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static List<PlayLists> GetPlayListsAll()
        {
            string sql = "select * from tbl_PlayLists";
            List<PlayLists> list = DBQuerySQLite<PlayLists>(sql);
            return list;
        }
        public static List<PlayLists> GetPlayListsByPlayListName(string name)
        {
            string sql = "select * from tbl_PlayLists where PlayListName='" + name + "'";
            List<PlayLists> p = DBQuerySQLite<PlayLists>(sql);
            return p;
        }
        public static int DeletePlayListsByPlayListName(string playListName)
        {
            string sql = "DELETE FROM tbl_PlayLists WHERE PlayListName='" + playListName + "'";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int DeletePlayLists(PlayLists item)
        {
            string sql = "DELETE FROM tbl_PlayLists WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int DeletePlayListsAll()
        {
            string sql = "DELETE FROM tbl_PlayLists";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int InsertLibrary(Library item)
        {
            string sql = "INSERT INTO tbl_Library (Artist,Title,Album,Duration,DurationString,FileType,FilePath,MusicType,PlayCount,Rate,FavSong,StartTime,EndTime,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@Artist,@Title,@Album,@Duration,@DurationString,@FileType,@FilePath,@MusicType,@PlayCount,@Rate,@FavSong,@StartTime,@EndTime,@CreateDate,@UpdateDate)";
            item.ID = GetCount("tbl_Library") + 1;
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int UpdateLibrary(Library item)
        {
            string sql = "UPDATE tbl_Library SET Artist=@Artist,Title=@Title,Album=@Album,Duration=@Duration,DurationString=@DurationString,FileType=@FileType,FilePath=@FilePath," +
                "MusicType=@MusicType,PlayCount=@PlayCount,Rate=@Rate,FavSong=@FavSong,StartTime=@StartTime,EndTime=@EndTime,CreateDate=@CreateDate,UpdateDate=@UpdateDate WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int DeleteLibrary(Library item)
        {
            string sql = "DELETE FROM tbl_Library WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static Library GetLibraryByFilePath(string filePath)
        {
            Library p = GetInfoByCol<Library>("tbl_Library", "FilePath", filePath);
            return p;
        }
        public static int InsertFolders(Folders item)
        {
            string sql = "INSERT INTO tbl_Folders (FolderName,FolderFilePath,FileLastModifyDate,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@FolderName,@FolderFilePath,@FileLastModifyDate,@CreateDate,@UpdateDate)";
            item.ID = GetCount("tbl_Folders") + 1;
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int GetCount(string tableName)
        {
            try
            {
                int i = 0;
                System.Data.IDbConnection conn = getConnection();
                string sql = "select count(1) from " + tableName + " ";
                i = conn.ExecuteScalar<int>(sql);
                conn.Dispose();
                return i;
            }
            catch
            {
                return 0;
            }
        }
        public static int GetCountAndWhere(string tableName,string whereStr)
        {
            try
            {
                int i = 0;
                System.Data.IDbConnection conn = getConnection();
                string sql = "select count(1) from " + tableName + " where "+whereStr;
                i = conn.ExecuteScalar<int>(sql);
                conn.Dispose();
                return i;
            }
            catch
            {
                return 0;
            }
        }
        public static int UpdateFolders(Folders item)
        {
            string sql = "UPDATE tbl_Folders SET FolderName=@FolderName,FolderFilePath=@FolderFilePath,FileLastModifyDate=@FileLastModifyDate,CreateDate=@CreateDate,UpdateDate=@UpdateDate WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static List<Folders> GetFoldersAll()
        {
            string sql = "select * from tbl_Folders";
            List<Folders> list = DBQuerySQLite<Folders>(sql);
            return list;
        }
        public static List<Folders> GetFoldersAllName()
        {
            string sql = "select * from tbl_Folders order by UpdateDate desc limit 10";
            List<Folders> list = DBQuerySQLite<Folders>(sql);
            return list;
        }
        public static int DeleteFoldersByFolderName(string folderName)
        {
            string sql = "DELETE FROM tbl_Folders WHERE FolderName='" + folderName + "'";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int DeleteFolders(Folders item)
        {
            string sql = "DELETE FROM tbl_Folders WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int DeleteFoldersAll()
        {
            string sql = "DELETE FROM tbl_Folders";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static Folders GetFoldersByFolderFilePath(string filePath)
        {
            Folders p = GetInfoByCol<Folders>("tbl_Folders", "FolderFilePath", filePath);
            return p;
        }
        public static int InsertLibraryFolders(Folders item)
        {
            string sql = "INSERT INTO tbl_LibraryFolders (FolderName,FolderFilePath,FileLastModifyDate,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@FolderName,@FolderFilePath,@FileLastModifyDate,@CreateDate,@UpdateDate)";
            item.ID = GetCount("tbl_LibraryFolders") + 1;
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int UpdateLibraryFolders(Folders item)
        {
            string sql = "UPDATE tbl_LibraryFolders SET FolderName=@FolderName,FolderFilePath=@FolderFilePath,FileLastModifyDate=@FileLastModifyDate,CreateDate=@CreateDate,UpdateDate=@UpdateDate WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static List<Folders> GetLibraryFoldersAll()
        {
            string sql = "select * from tbl_LibraryFolders";
            List<Folders> list = DBQuerySQLite<Folders>(sql);
            return list;
        }
        public static int DeleteLibraryFolders(Folders item)
        {
            string sql = "DELETE FROM tbl_LibraryFolders WHERE ID=@ID";
            int i = DBFuncSQLite(sql, item);
            return i;
        }
        public static int DeleteLibraryFoldersAll()
        {
            string sql = "DELETE FROM tbl_LibraryFolders";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int InsertFavorite(PlayList item)
        {
            string sql = "INSERT INTO tbl_Favorite (Artist,Title,Album,Duration,DurationString,FileType,FilePath,MusicType,PlayCount,Rate,FavSong,StartTime,EndTime,CreateDate,UpdateDate)" +
                "VALUES " +
                "(@Artist,@Title,@Album,@Duration,@DurationString,@FileType,@FilePath,@MusicType,@PlayCount,@Rate,@FavSong,@StartTime,@EndTime,@CreateDate,@UpdateDate)";
            SQLiteConnection conn = getConnection();
            conn.Open();
            SQLiteTransaction trans = BeginTransaction(conn);
            int i = DBFuncSQLite(sql, item, trans, conn);
            return i;
        }
        public static List<PlayList> GetFavList()
        {
            string sql = "select * from tbl_Favorite";
            List<PlayList> p = DBQuerySQLite<PlayList>(sql);
            return p;
        }
        public static int DeleteFavorite(string filePath)
        {
            string sql = "DELETE FROM tbl_Favorite where FilePath='" + filePath + "'";
            int i = DBFuncSQLite(sql);
            return i;
        }
        public static int HaveFavorite(string filePath)
        {
            int i = GetCountAndWhere("tbl_Favorite", " FilePath='" + filePath + "'");
            return i; 
        }
    }
}
