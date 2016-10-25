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
import android.util.Log;
import android.util.Xml;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;
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

            /* ************ esperando respuesta ************** */

            BufferedReader r = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            /*StringBuilder total = new StringBuilder();
            String line;
            while ((line = r.readLine()) != null) {
                total.append(line);
            }*/

            while(socket.isConnected()) {
                writer.println(Serializacion.Serializar(new MsgConexion("yo", "yo", "2016-10-18", mensaje)));
                writer.flush();
                //socket.shutdownOutput();
            Log.i("msg","esperando respuesta");
            String linea = r.readLine();
            Log.i("msg","leyo respuesta");
            Log.i("msg",linea);
            MsgConexion msj = (MsgConexion) Serializacion.Deserealizar(linea);
                Log.i("Tarea Deserializar","ya deserialice");
            //respuesta = "TO: " + msj.getTo() + " FROM: " + msj.getFrom() + " Mensaje: " + msj.getMensaje();
                Log.i("Tarea Mostrar ",respuesta);
        }

            Log.i("msg",respuesta);


            socket.close();
            writer.close();
        } catch (Exception e) {
            e.printStackTrace();
            respuesta = "Error! "+e.toString();
        }
        return null;
    }


    @Override
    protected void onPostExecute(Void result) {
        textRespuesta.setText(respuesta);
        super.onPostExecute(result);
    }

}
