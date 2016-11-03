package com.example.admin.pruebatracking;

import android.app.Activity;
import android.content.Context;
import android.graphics.drawable.AnimationDrawable;
import android.location.LocationListener;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.Mensajes.Mensaje;
import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Mensajes.MsgLocalizacion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

/**
 * Created by Admin on 1/11/2016.
 */
public class ServicioRecibir extends AsyncTask<Void, Void, Void> {

    Socket socket;
    Context context;
    boolean conectar;

    public ServicioRecibir(Socket socket, Context context, boolean conectar) {

        this.socket = socket;
        this.context = context;
        this.conectar = conectar;
            }

            @Override
            protected Void doInBackground(Void... arg0) {

                try {
                    PrintWriter writer = new PrintWriter(socket.getOutputStream());
                    BufferedReader reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                    while (socket.isConnected())
                    {
                        Log.i("msg", "esperando respuesta");
                        String json = reader.readLine();
                        Log.i("msg", "llego: " + json);
                        Mensaje msg = (Mensaje)Serializacion.Deserealizar(json);

                switch (msg.getTipo()){
                        case "MsgConexion":
                            ((Activity) context).runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Toast.makeText(context, "Llego un mensaje de conexion", Toast.LENGTH_LONG).show();
                                }
                            });

                            break;
                        case "MsgLocalizacion":
                            ((Activity) context).runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Toast.makeText(context, "Llego un mensaje de localización", Toast.LENGTH_LONG).show();
                                }
                            });

                            break;
                    case "MsgDBPeticion":
                        ((Activity) context).runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(context, "Llego un mensaje de peticion", Toast.LENGTH_LONG).show();
                            }
                        });

                        break;
                }
            }

            writer.close();
        } catch (Exception e) {
            ((Activity) context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(context, "ERROR EN SERVICIO RECIBIR MENSAJES", Toast.LENGTH_LONG).show();
                }
            });
            e.printStackTrace();
        }
        return null;
    }


    @Override
    protected void onPostExecute(Void result) {
        super.onPostExecute(result);
    }
}

