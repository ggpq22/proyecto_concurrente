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

        try{
            Socket cliente = new Socket(addr,port);

            //Creamos el objeto json
            //JSONObject jsonParam = new JSONObject();
            //jsonParam.put("mensaje", mensaje);

            DataOutputStream dos = new DataOutputStream(cliente.getOutputStream()); // para enviar datos al servidor
            DataInputStream dis = new DataInputStream(cliente.getInputStream()); // para recibir datos del servidor

            dos.writeUTF(serializeToJson(new Mensaje(mensaje)));

            dos.close();
            dis.close();
            cliente.close();

        }catch(Exception e){
            e.printStackTrace();
            respuesta = e.toString();
        }
        return null;
    }

    public String serializeToJson(Mensaje myClass) {
        Gson gson = new Gson();
        String j = gson.toJson(myClass);
        return j;
    }

    @Override
    protected void onPostExecute(Void result) {
        textRespuesta.setText(respuesta);
        super.onPostExecute(result);
    }

}
