package com.example.admin.pruebatracking;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.IU.ListViewAdapterGrupos;
import com.example.admin.pruebatracking.IU.MainActivity;
import com.example.admin.pruebatracking.Mensajes.Mensaje;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.Serializacion.Serializacion;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.ArrayList;

/**
 * Created by Admin on 1/11/2016.
 */
public class ServicioRecibir implements Runnable {

    Context context;
    AplicacionPrincipal global;

    public ServicioRecibir(Context context) {

        this.context = context;
        this.global = ((AplicacionPrincipal) context.getApplicationContext());
    }

    public void run() {
                try {
                    if(global.getConectado() && global.getSocket().isConnected()) {
                        Log.e("msg", "entro a servicio recibir");
                        PrintWriter writer = new PrintWriter(global.getSocket().getOutputStream());
                        Log.e("msg", "paso el writer");
                        BufferedReader reader = new BufferedReader(new InputStreamReader(global.getSocket().getInputStream()));

                        while (global.getSocket().isConnected()) {
                            Log.e("msg", "esperando respuesta");
                            String json = reader.readLine();
                            Log.e("msg", "llego: " + json);
                            final Mensaje msg = (Mensaje) Serializacion.Deserealizar(json);

                            switch (msg.getTipo()) {
                                case "MsgConexion":
                                    Log.e("msg", "Llego mensaje de conexion");

                                    break;
                                case "MsgLocalizacion":
                                    Log.e("msg", "Llego mensaje de conexion");

                                    break;
                                case "MsgDBPeticion":
                                    Log.e("msg", "Llego mensaje de peticion");

                                    break;
                                case "MsgDBRespuesta":
                                    Log.e("msg", "Llego mensaje de respuesta");
                                    switch (((MsgDBRespuesta)msg).getCodigoPeticion()) {
                                        case "Login":
                                            Log.e("msg", "Llego mensaje de respuesta con codigo de Login");
                                            global.setRespuestaEntrar(true);
                                            global.setMsgDBRespuestaEntrar(((MsgDBRespuesta)msg));
                                            break;

                                        case "CrearCuenta":
                                            Log.e("msg", "Llego mensaje de respuesta con codigo de CrearCuenta");;;
                                            global.setRespuestaEntrar(true);
                                            global.setMsgDBRespuestaEntrar(((MsgDBRespuesta) msg));
                                            break;

                                        case "GetGrupoPorIntegrante":
                                            final ListViewAdapterGrupos adapter = global.getAdapterGrupos();
                                            if(adapter != null && msg.getIsValido())
                                            {
                                                ((Activity)global.getContext()).runOnUiThread(new Runnable() {
                                                @Override
                                                public void run() {
                                                    adapter.setGrupos(((MsgDBRespuesta) msg).getReturnGrupo());
                                                    adapter.notifyDataSetChanged();
                                                    global.setRespuestaRecuperarGrupos(true);
                                                }
                                            });
                                            }
                                            break;
                                    }
                                    break;

                                case "MsgNotificacion":
                                    Log.e("msg", "Llego mensaje de notificacion");

                                    break;

                            }
                        }
                    }

        } catch (Exception e) {
            e.printStackTrace();
            Log.e("msg", " se rompio en ServicioRecibir Excepcion: "+e.toString());
        }
    }


}

