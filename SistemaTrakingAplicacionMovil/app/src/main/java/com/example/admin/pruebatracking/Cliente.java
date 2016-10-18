package com.example.admin.pruebatracking;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.URLEncoder;
import java.net.UnknownHostException;
import android.os.AsyncTask;
import android.os.Bundle;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;

public class Cliente extends AsyncTask<Void, Void, Void> {

    String addr;
    int port;
    String mensaje;
    String respuesta = "";
    TextView textRespuesta;

    Cliente(String addr, int port, String mensaje, TextView textRespuesta) {
        this.addr = addr;
        this.port = port;
        this.mensaje = mensaje;
        this.textRespuesta = textRespuesta;
    }

    @Override
    protected Void doInBackground(Void... arg0) {

        Socket socket = null;

        try {
            socket = new Socket( addr, port);
            PrintWriter writer = new PrintWriter(socket.getOutputStream());
            writer.print(serializeToJson(new Mensaje(mensaje)));
            writer.flush();
            socket.shutdownOutput();

            /* ************ esperando respuesta ************** */

            BufferedReader r = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            /*StringBuilder total = new StringBuilder();
            String line;
            while ((line = r.readLine()) != null) {
                total.append(line);
            }*/
            String linea = r.readLine();
            Mensaje msj = deserializeFromJson(linea);
            respuesta = msj.getMensaje();

            socket.close();
            writer.close();
        } catch (Exception e) {
            e.printStackTrace();
            respuesta = "Error! "+e.toString();
        }
        return null;
    }

    public String serializeToJson(Mensaje myClass) {
        Gson gson = new Gson();
        String j = gson.toJson(myClass);
        return j;
    }

    public Mensaje deserializeFromJson(String jsonString) {
        Gson gson = new Gson();
        Mensaje m = gson.fromJson(jsonString, Mensaje.class);
        return m;
    }

    @Override
    protected void onPostExecute(Void result) {
        textRespuesta.setText(respuesta);
        super.onPostExecute(result);
    }

}
