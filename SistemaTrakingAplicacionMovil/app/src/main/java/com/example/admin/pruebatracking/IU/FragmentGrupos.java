package com.example.admin.pruebatracking.IU;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;


import com.example.admin.pruebatracking.R;


public class FragmentGrupos extends Fragment {

    ListViewAdapter adapter;

    String[] titulos;
    int[] imagenes;
    String[] estados;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View rootView = inflater.inflate(R.layout.fragment_grupos, container, false);

        ProgressDialog dialog=new ProgressDialog(rootView.getContext());
        dialog.setMessage("message");
        dialog.setCancelable(false);
        dialog.setInverseBackgroundForced(false);
        dialog.show();

        ListView lista = (ListView) rootView.findViewById(R.id.listView_listarGrupos);

        titulos = new String[10];
        imagenes = new int[10];
        estados = new String[10];

        for(int i = 0; i < 10; i++){
            titulos[i] = "Nombre del grupo";
            estados[i] = "SIN SEGUIMIENTO";
            imagenes[i] = R.drawable.defaultuser;

        }

        adapter = new ListViewAdapter(getContext(), titulos, imagenes, estados);
        lista.setAdapter(adapter);

        lista.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent i = new Intent(getActivity(), ListarUnGrupo.class);
                i.putExtra("position", position);
                startActivity(i);


            }
        });
        return rootView;
    }
    public class ListViewAdapter extends BaseAdapter {

        Context context;
        String[] titulos;
        int[] imagenes;
        String[] estados;

        LayoutInflater inflater;

        public ListViewAdapter(Context context, String[] titulos, int[] imagenes, String[] estados) {
            this.context = context;
            this.titulos = titulos;
            this.imagenes = imagenes;
            this.estados = estados;
        }

        @Override
        public int getCount() {
            return titulos.length;
        }

        @Override
        public Object getItem(int position) {
            return null;
        }

        @Override
        public long getItemId(int position) {
            return 0;
        }

        public View getView(int position, View convertView, ViewGroup parent) {

            TextView tvTitulo;
            ImageView ivImagen;
            TextView tvEstado;

            inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

            View itemView = inflater.inflate(R.layout.lista_personalizada_grupos, parent, false);

            ivImagen = (ImageView) itemView.findViewById(R.id.imagen_lista_personalizada_grupo);
            tvTitulo = (TextView) itemView.findViewById(R.id.tv_titulo_grupo);
            tvEstado = (TextView) itemView.findViewById(R.id.tv_estado_grupo);
            ivImagen.setImageResource(imagenes[position]);
            tvTitulo.setText(titulos[position]);
            tvEstado.setText("ESTADO: " + estados[position]);

            return itemView;
        }
    }
}
