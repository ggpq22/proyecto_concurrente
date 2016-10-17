package com.example.admin.pruebatracking.Serializacion;

import com.google.gson.Gson;

import java.net.Proxy;

/**
 * Created by Mario on 13/10/2016.
 */
public class Serializacion<Tipo>{

    public String Serializar(Tipo objeto){
        Gson gson = new Gson();
        return gson.toJson(objeto);
    }


    public  Tipo Deserealizar(String json){
        Gson gson = new Gson();
        return gson.fromJson(json, Tipo);
    }

}
