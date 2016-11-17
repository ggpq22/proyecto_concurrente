package com.example.admin.pruebatracking.IU;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.support.v4.view.GravityCompat;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.AppCompatActivity;
import android.text.format.DateFormat;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;


import com.example.admin.pruebatracking.AplicacionPrincipal;
import com.example.admin.pruebatracking.Cliente;
import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;


public class FragmentGrupos extends Fragment {

    ListViewAdapterGrupos adapter;
    AplicacionPrincipal global;
    ArrayList<Boolean> seleccionados;
    ListView lista;
    Cliente cliente;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View rootView = inflater.inflate(R.layout.fragment_grupos, container, false);
        lista = (ListView) rootView.findViewById(R.id.listView_listarGrupos);

        global = ((AplicacionPrincipal)getContext().getApplicationContext());

        setHasOptionsMenu(true);

        seleccionados = new ArrayList<Boolean>();

        for(int i = 0; i < global.getGrupos().size(); i++) {
            seleccionados.add(new Boolean(false));
        }

        adapter = new ListViewAdapterGrupos(getContext(), global.getGrupos(), seleccionados);
        lista.setAdapter(adapter);
        ((AplicacionPrincipal)getContext().getApplicationContext()).setAdapterGrupos(adapter);


        lista.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> av, View v, int position, long id) {
                seleccionados.set(position, true);
                adapter.setSeleccionados(seleccionados);
                adapter.notifyDataSetChanged();
                return true;
            }
        });

        lista.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                seleccionados.set(position, false);
                adapter.setSeleccionados(seleccionados);
                adapter.notifyDataSetChanged();
            }
        });

        return rootView;
    }

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
            MenuItem itemBlog = menu.add(Menu.NONE, // Group ID
                    R.id.action_delete, // Item ID
                    101, // Order
                    "delete"); // Title
            MenuItemCompat.setShowAsAction(itemBlog, MenuItem.SHOW_AS_ACTION_ALWAYS);
            itemBlog.setIcon(R.drawable.ic_action_ic_delete);
            super.onCreateOptionsMenu(menu, inflater);


    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
        switch (item.getItemId()) {
            case R.id.action_delete:
                borrarGrupo();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    public void borrarGrupo(){
        boolean itemSelect = false;
        ArrayList<Integer> posiciones = new ArrayList<Integer>();
        ArrayList<Grupo> NuevaListaGrupos = global.getGrupos();
        ArrayList<String> arrayDestino = new ArrayList<String>();
        arrayDestino.add(global.getCuenta().getUsuario());
        ArrayList<Cuenta> Arraycuenta= new ArrayList<Cuenta>();
        Arraycuenta.add(global.getCuenta());
        ArrayList<Grupo> gruposDelete = new ArrayList<Grupo>();
        String fecha = (DateFormat.format("yyyy-MM-dd", new java.util.Date()).toString());

        for(int i = 0; i < seleccionados.size(); i++) {
            if(seleccionados.get(i)) {
                itemSelect = true;
                break;
            }
        }
        if(itemSelect){
            for(int i = 0; i < seleccionados.size(); i++){
                if(seleccionados.get(i)){
                    posiciones.add(i);
                }
            }
            for(int i = 0; i < posiciones.size(); i++){
                gruposDelete.add(NuevaListaGrupos.get((int)posiciones.get(i)));
                NuevaListaGrupos.remove((int)posiciones.get(i));
                seleccionados.remove((int)posiciones.get(i));
            }

            cliente = new Cliente(getContext(), arrayDestino, global.getCuenta().getUsuario(), fecha);

            MsgDBPeticion peticionDeleteGrupos = new MsgDBPeticion(arrayDestino, global.getCuenta().getUsuario(), fecha, "BorrarCuentaDeGrupo", Arraycuenta, gruposDelete, false);
            cliente.enviarMensajes(peticionDeleteGrupos);

            final ProgressDialog progressDialog = new ProgressDialog(getContext(), R.style.AppTheme_Dark_Dialog);
            progressDialog.setIndeterminate(true);
            progressDialog.setCancelable(false);
            progressDialog.setMessage("Por favor espere...");
            progressDialog.show();

            new Thread() {
                public void run() {

                    while (!global.getRespuestaDeleteGrupos()) {
                        try {
                            Thread.sleep(100);
                        } catch (Exception e) {
                            e.printStackTrace();
                            Log.e("msg", "Error en esperando Respuesta delete grupos: " + e.toString());
                        }
                    }

                    global.setRespuestaDeleteGrupos(false);
                    MsgDBRespuesta msg = global.getMsgDBRespuestaDeleteGrupos();
                    if (msg != null && msg.getIsValido()) {
                        ((Activity)global.getContext()).runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(getContext(), "Grupos eliminados correctamente", Toast.LENGTH_LONG).show();
                                progressDialog.dismiss();
                            }
                        });
                    }
                    else{
                        ((Activity)global.getContext()).runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(getContext(), "No se pudo borrar el/los grupos. Intente mas tarde", Toast.LENGTH_LONG).show();
                                progressDialog.dismiss();
                            }
                        });
                    }

                }
            }.start();

            global.setGrupos(NuevaListaGrupos);
            adapter.setGrupos(global.getGrupos());
            adapter.setSeleccionados(seleccionados);
            lista.setAdapter(adapter);
            adapter.notifyDataSetChanged();

        }
        else{
            Toast.makeText(getContext(),"Mantenga apretado un grupo para seleccionarlo",Toast.LENGTH_LONG).show();
        }
    }
}
