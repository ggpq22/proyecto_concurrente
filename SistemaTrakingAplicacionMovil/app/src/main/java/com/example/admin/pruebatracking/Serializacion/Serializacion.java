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
<<<<<<< HEAD
        Log.i("inicio de deser",json);
        Gson gson = null;
        try {
            gson = new Gson();
        }
        catch (Exception e)
        {
            Log.i("ERROR:" ,e.getMessage());
        }
        Log.i("si crea gson","sep");
        Mensaje msg = gson.fromJson(json, MsgConexion.class);
        Log.i("si funciona",msg.getTipo());
=======
        Gson gson = new Gson();

        Mensaje msg = gson.fromJson(json, Mensaje.class);

>>>>>>> f1dff8742bf8c90baa572d100cafe2261e5ddcb0
        Object o = new Object();
        Log.i("TIPO",msg.getTipo());

        switch (msg.getTipo()){
            case "MsgConexion":
                o = gson.fromJson(json,MsgConexion.class);
                break;
            case "MsgLocalizacion":
                o = gson.fromJson(json,MsgLocalizacion.class);
                break;
        }
        return  o;
    }
}
