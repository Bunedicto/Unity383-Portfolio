using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;

public class RetreiveMap : MonoBehaviour
{
    public int zoom = 0;
    public int x = 0;
    public int y = 0;

    public float lati;
    public float longi;
    public float terraDyn;

    public Material mapMaterial;
    public GameObject tileObject;
    public GameObject tileObject2;
    public GameObject tileObject3;

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        // All Certificates are accepted. Not good practice, but outside scope of this example.
        return true;
    }

    private void retreiveTile(int zoom, int x, int y)
    {
        // Elevation tiles, see: https://www.mapzen.com/blog/terrain-tile-service/
        string url = "https://s3.amazonaws.com/elevation-tiles-prod/terrarium/" + zoom + "/" + x + "/" + y + ".png";
        //string url = "https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png";
        WebRequest www = WebRequest.Create(url);
        ((HttpWebRequest)www).UserAgent = "TerrainAltitudeMaps";
        WebResponse response = www.GetResponse();
        Texture2D tex = new Texture2D(10, 10);
        ImageConversion.LoadImage(tex, new BinaryReader(response.GetResponseStream()).ReadBytes(10000000));
        mapMaterial.mainTexture = tex;
    }

    private void retreiveTexture(int zoom, int x, int y)
    {
        // Elevation tiles, see: https://www.mapzen.com/blog/terrain-tile-service/
        //string url = "https://s3.amazonaws.com/elevation-tiles-prod/terrarium/" + zoom + "/" + x + "/" + y + ".png";
        string url = "https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png";
        WebRequest www = WebRequest.Create(url);
        ((HttpWebRequest)www).UserAgent = "TerrainAltitudeMaps";
        WebResponse response = www.GetResponse();
        Texture2D tex = new Texture2D(10, 10);
        ImageConversion.LoadImage(tex, new BinaryReader(response.GetResponseStream()).ReadBytes(10000000));
        mapMaterial.mainTexture = tex;
    }

    void makeMesh(int width, int height)
    {
        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        Vector2[] uvs = new Vector2[(width + 1) * (height + 1)];
        int[] triangles = new int[6 * width * height];
        int triangleindex = 0;
        for (int y = 0; y < height + 1; y++)
        {
            for (int x = 0; x < width + 1; x++)
            {
                float xc = (float)x / (width + 1);
                float yc = (float)y / (height + 1);
                //float zc = Mathf.Sin (10.0f * xc);
                Texture2D tex = (Texture2D)mapMaterial.mainTexture;
                Color c = tex.GetPixel((int)(xc * tex.width), (int)(yc * tex.height));
                float zc = (c.r * 256 + c.g + c.b / 256) - 128;
                if (zc < 0)
                {
                    zc = 0;
                }

                Debug.Log("At " + x + " " + y + " " + triangleindex);
                vertices[y * (width + 1) + x] = new Vector3(10.0f * (xc - 0.5f), 10.0f * (yc - 0.5f), -terraDyn * zc);
                uvs[y * (width + 1) + x] = new Vector2(xc, yc);

                // Dynamic terrain
                if (zoom<9)
                {
                    terraDyn = 0.1f;
                }
                if (zoom>9)
                {
                    terraDyn = 1.0f;
                }
                if (zoom > 13)
                {
                    terraDyn = 2.0f;
                }

                if ((x < width) && (y < height))
                {
                    triangles[triangleindex++] = (y) * (width + 1) + (x + 1);
                    triangles[triangleindex++] = (y) * (width + 1) + (x);
                    triangles[triangleindex++] = (y + 1) * (width + 1) + (x);

                    triangles[triangleindex++] = (y + 1) * (width + 1) + (x + 1);
                    triangles[triangleindex++] = (y) * (width + 1) + (x + 1);
                    triangles[triangleindex++] = (y + 1) * (width + 1) + (x);
                }
            }
        }

        Mesh m = new Mesh();
        m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;
        m.RecalculateNormals();
        tileObject.GetComponent<MeshFilter>().mesh = m;
        tileObject2.GetComponent<MeshFilter>().mesh = m;
        tileObject3.GetComponent<MeshFilter>().mesh = m;
    }

    
    private void getTileCoordinates(float longitude, float latitude, int zoom, out int x, out int y)
    {
        x = (int)(Mathf.Floor((longitude + 180.0f) / 360.0f * Mathf.Pow(2.0f, zoom)));
        y = (int)(Mathf.Floor((1.0f - Mathf.Log(Mathf.Tan(latitude * Mathf.PI / 180.0f) + 1.0f / Mathf.Cos(latitude * Mathf.PI / 180.0f)) / Mathf.PI) / 2.0f * Mathf.Pow(2.0f, zoom)));
    }
    

    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;

        // Have latitiude and longitude floats
        getTileCoordinates(longi, lati, zoom, out x, out y);

        retreiveTile(zoom, x, y);
        makeMesh(128, 128);
        retreiveTexture(zoom, x, y);
    }

    public void zoomIn ()
    {
        zoom = zoom + 1;
        x *= 2;
        y *= 2;
        retreiveTile(zoom, x, y);
        makeMesh(128, 128);
        retreiveTexture(zoom, x, y);
    }

    public void zoomOut()
    {
        zoom = zoom - 1;
        x /= 2;
        y /= 2;
        retreiveTile(zoom, x, y);
        makeMesh(128, 128);
        retreiveTexture(zoom, x, y);
    }

    /*
    private void getTileCoordinates(float longitude, float latitude, int zoom, out int x, out int y)
    {
        x = (int)(Mathf.Floor((longitude + 180.0f) / 360.0f * Mathf.Pow(2.0f, zoom)));
        y = (int)(Mathf.Floor((1.0f - Mathf.Log(Mathf.Tan(latitude * Mathf.PI / 180.0f) + 1.0f / Mathf.Cos(latitude * Mathf.PI / 180.0f)) / Mathf.PI) / 2.0f * Mathf.Pow(2.0f, zoom)));
    }

    private void getGeoCoordinates(int x, int y, int zoom, out float longitude, out float latitude)
    {
        float n = Mathf.PI - 2.0f * Mathf.PI * y / Mathf.Pow(2.0f, zoom);

        longitude = x / Mathf.Pow(2.0f, zoom) * 360.0f - 180.0f;
        latitude = 180.0f / Mathf.PI * Mathf.Atan(0.5f * (Mathf.Exp(n) - Mathf.Exp(-n)));
    }

    private void retreiveTile(int zoom, int x, int y)
    {
        // Elevation tiles, see: https://www.mapzen.com/blog/terrain-tile-service/
        string url = "https://s3.amazonaws.com/elevation-tiles-prod/terrarium/" + zoom + "/" + x + "/" + y + ".png";
        //string url = "https://a.tile.openstreetmap.org/" + zoom + "/" + x + "/" + y + ".png";
        WebRequest www = WebRequest.Create(url);
        ((HttpWebRequest)www).UserAgent = "TerrainAltitudeMaps";
        WebResponse response = www.GetResponse();
        Texture2D tex = new Texture2D(10, 10);
        ImageConversion.LoadImage(tex, new BinaryReader(response.GetResponseStream()).ReadBytes(10000000));
        mapMaterial.mainTexture = tex;
    }
    */
}
