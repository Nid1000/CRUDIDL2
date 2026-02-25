import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CasaDto } from '../../models/casa/CasaDto.model';

@Injectable({
  providedIn: 'root',
})
export class CasaService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5133/api/casa';

  getAll(): Observable<CasaDto[]> {
    return this.http.get<CasaDto[]>(this.apiUrl);
  }

  create(data: CasaDto): Observable<CasaDto> {
    return this.http.post<CasaDto>(this.apiUrl, data);
  }

  update(id: number, data: CasaDto): Observable<CasaDto> {
    return this.http.put<CasaDto>(this.apiUrl, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
