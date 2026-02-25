import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MascotaTipoDto } from '../../models/mascota/MascotaTipoDto.model';

@Injectable({
  providedIn: 'root',
})
export class MascotaTipoService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5133/api/mascotatipo';

  getAll(): Observable<MascotaTipoDto[]> {
    return this.http.get<MascotaTipoDto[]>(this.apiUrl);
  }

  create(data: MascotaTipoDto): Observable<MascotaTipoDto> {
    return this.http.post<MascotaTipoDto>(this.apiUrl, data);
  }

  update(id: number, data: MascotaTipoDto): Observable<MascotaTipoDto> {
    return this.http.put<MascotaTipoDto>(this.apiUrl, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
