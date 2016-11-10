package com.example.admin.pruebatracking.Serializacion;

import android.util.Log;

import com.example.admin.pruebatracking.Mensajes.*;
import com.google.gson.Gson;

import java.net.Proxy;

/**
 * Created by Mario on 13/10/2016.
 */
public class Serializacion{

    public static String Serializar(Mensaje objeto){
        Gson gson = new Gson();
        return gson.toJson(objeto);
    }


    public static Object Deserealizar(String json){

        Gson gson = new Gson();

        Mensaje msg = gson.fromJson(json, Mensaje.class);

        Object o = new Object();

        switch (msg.getTipo()){
            case "MsgConexion":
                o = gson.fromJson(json,MsgConexion.class);
                break;
            case "MsgLocalizacion":
                o = gson.fromJson(json,MsgLocalizacion.class);
                break;
            case "MsgDBPeticion":
                o = gson.fromJson(json,MsgLocalizacion.class);
                break;
            case "MsgDBRespuesta":
                o = gson.fromJson(json,MsgLocalizacion.class);
                break;
            case "MsgNotificacion":
                o = gson.fromJson(json,MsgLocalizacion.class);
                break;
        }
        return  o;
    }
}
