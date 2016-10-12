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
import java.net.UnknownHostException;
import android.os.AsyncTask;
import android.widget.TextView;
import android.widget.Toast;

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
            Socket cliente = new Socket("10.75.60.187",8999);

            InputStreamReader isr = new InputStreamReader(System.in);
            BufferedReader br = new BufferedReader(isr);
            ObjectOutputStream oos = new ObjectOutputStream(cliente.getOutputStream()); // para enviar datos al server
            ObjectInputStream ois = new ObjectInputStream(cliente.getInputStream()); // para recibir datos del server

            oos.writeObject(mensaje);
            respuesta = ois.readObject().toString();

            isr.close();
            br.close();
            oos.close();
            ois.close();
            cliente.close();

        }catch(Exception e){
            e.printStackTrace();
            respuesta = e.toString();
        }
        return null;
    }

    @Override
    protected void onPostExecute(Void result) {
        textRespuesta.setText(respuesta);
        super.onPostExecute(result);
    }

}
