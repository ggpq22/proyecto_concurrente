package com.example.admin.pruebatracking.IU;

import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
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
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;


public class FragmentGrupos extends Fragment {

    ListViewAdapterGrupos adapter;
    ArrayList<Grupo> grupos;


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View rootView = inflater.inflate(R.layout.fragment_grupos, container, false);

        ListView lista = (ListView) rootView.findViewById(R.id.listView_listarGrupos);


        grupos = ((AplicacionPrincipal)getContext().getApplicationContext()).getGrupos();
        ArrayList<Boolean> seleccionados = new ArrayList<Boolean>();
        for(int i = 0; i < grupos.size(); i++) {
            seleccionados.add(new Boolean(false));
        }

        adapter = new ListViewAdapterGrupos(getContext(), grupos, seleccionados);
        lista.setAdapter(adapter);
        ((AplicacionPrincipal)getContext().getApplicationContext()).setAdapterGrupos(adapter);



        lista.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> av, View v, int pos, long id) {
                //item_sel.setBackgroundColor(Color.parseColor("#81F781"));
                Toast.makeText(getContext(), "posicion "+pos, Toast.LENGTH_LONG).show();
                return true;
            }
        });

        return rootView;
    }
}
