import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MascotaDto } from '../../models/mascota/MascotaDto.model';

@Injectable({
  providedIn: 'root',
})
export class MascotaService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5133/api/mascota';

  getAll(): Observable<MascotaDto[]> {
    return this.http.get<MascotaDto[]>(this.apiUrl);
  }

  create(data: MascotaDto): Observable<MascotaDto> {
    return this.http.post<MascotaDto>(this.apiUrl, data);
  }

  update(id: number, data: MascotaDto): Observable<MascotaDto> {
    return this.http.put<MascotaDto>(this.apiUrl, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
